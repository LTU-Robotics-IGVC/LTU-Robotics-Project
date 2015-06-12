#include <Wire.h>
#include <Servo.h>

Servo rm,lm;
boolean rightForward;//keeps track of last direction does nothing for now
boolean leftForward;//does nothing for now
int pause;//delay for switching direction
const int messageSize = 30;
String masterMessage = "";
volatile char slaveMessage[messageSize];
volatile char emptyMessage[messageSize];
volatile boolean slaveMessageReady = false;
double spd;
int spdpwm;
String lastCommand;
boolean autonomous;
boolean light;
int timer;

//left
//right
//forward
//backwards
//stop

void setup()
{
  //rightmotor
  rm.attach(9);   //attach the signal pin of the ESC to pin 9
  rm.write(0);
  pinMode(10,OUTPUT);  //pin 10 will control the relay to flip the right motor
  digitalWrite(10,LOW);
  rightForward = true;
  pinMode(7,OUTPUT);
  digitalWrite(7,HIGH);
  
  //leftmotor
  lm.attach(11);   //attach the signal pin of the ESC to pin 11
  lm.write(0);
  pinMode(12,OUTPUT);  //pin 12 will control the relay to flip the left motor
  digitalWrite(12,LOW);
  leftForward = true;
  pinMode(5,OUTPUT);
  digitalWrite(5,HIGH);
  
  pause=50;//This worked when John and I tested it
  pinMode(8,INPUT);//wireless estop input
  
  Wire.begin(63);//left(63) right(72)
  Wire.onRequest(requestEvent);
  Wire.onReceive(receiveEvent);
  for(int i = 0; i < messageSize; i++)
  {
    emptyMessage[i] = ' ';
  }
  
  pinMode(13, OUTPUT);
  digitalWrite(13, HIGH);
  
  autonomous=false;
  light=true;
  timer=1;
  pinMode(4,OUTPUT);
  digitalWrite(4,LOW);
  
  Serial.begin(9600);
}

void loop()
{
  delay(20);
  if (digitalRead(8)==HIGH)//if wireless estop is pressed then motors will be set to 0
  {
    rm.write(0);
    lm.write(0);
    setMessage("ESTOP");
  }
  if(!autonomous)
  {
    timer=0;
    digitalWrite(4,LOW);//keeps light on
  }
  else
    timer=timer+1;
  if(timer>15)
  {
    timer = 1;
    if(light)
    {
      light=false;
      Serial.print("OFF");
      digitalWrite(4,HIGH);//turns light off
      digitalWrite(13,HIGH);
    }
    else
    {
      light=true;
      Serial.print("ON");
      digitalWrite(4,LOW);//turns light on
      digitalWrite(13,LOW);
    }
  }
  if(masterMessage != "")
  {
    int CommandIdentifierIndex = masterMessage.indexOf(':');
    if(CommandIdentifierIndex == -1) return; //Invalid structure
    
    String CommandIdentifier = masterMessage.substring(0, CommandIdentifierIndex);
    if(CommandIdentifier == "LED ON")
    {
      digitalWrite(13, HIGH);
    }
    else if(CommandIdentifier == "LED OFF")
    {
      digitalWrite(13, LOW);
    }
    else if(CommandIdentifier == "How are you?")
    {
      setMessage("I am fine as well");
    }
    else if(CommandIdentifier == "SWEEP")
    {
      for(int i = 0; i < 179; i++)
      {
        rm.write(i);
        delay(20);
      }
    }
    else if(CommandIdentifier == "A")//make the light flash or not flash
    {
      if(!autonomous)
      {
        autonomous=true;
        Serial.println("In Auto Mode");
      }
      else
      {
        autonomous=false;
        digitalWrite(4,LOW);//turns light to solid when exiting autonomous mode
        Serial.println("Not in AutoMode");
      }
    }
    else if(CommandIdentifier=="SETM" && digitalRead(8)==LOW)
    {          
        //grabs left motor speed
        int parameterIdentifierIndex = masterMessage.indexOf(':', CommandIdentifierIndex+1);
        if(parameterIdentifierIndex == -1) { return; Serial.println("Invalid parameter length");}
        
        Serial.println(parameterIdentifierIndex);
        String speedStrLeft = masterMessage.substring(CommandIdentifierIndex+1, parameterIdentifierIndex);
        Serial.println(speedStrLeft);
        
        //grabs right motor speed
        int parameterIdentifierIndex2 = masterMessage.indexOf(':',parameterIdentifierIndex+1);
        if(parameterIdentifierIndex2==-1) {return; Serial.println("Invalid parameter length");}
        
        Serial.println(parameterIdentifierIndex);
        String speedStrRight = masterMessage.substring(parameterIdentifierIndex+1, parameterIdentifierIndex2);
        Serial.println(speedStrRight);
        
        
        double spdLeft = speedStrLeft.toFloat();  //takes the string and makes it an int
        int spdpwmLeft;
        Serial.println(spdLeft);
        double spdRight = speedStrRight.toFloat();  //takes the string and makes it an int
        int spdpwmRight;
        Serial.println(spdRight);
        
        //left motor
        if (spdLeft<0)//checks if Left is forward or reverse
        {
          spdLeft=spdLeft*-1;
          if(spdLeft>2.23)
            spdLeft=2.23;
          spdpwmLeft=spdLeft*170.0/2.2352;//brings it to a scale for pwm. max set at 170 to avoid any acidential overshoot
            Serial.println("Left reverse");
            Serial.println(spdpwmLeft);
          if(spdpwmLeft==0)
          {
            lm.write(0);
            digitalWrite(5,LOW);
          }
          else
            digitalWrite(5,HIGH);
          if(leftForward!=true)//if just changing speed
            lm.write(spdpwmLeft);
          else//if switching direction
          {
            lm.write(0);//sets pwm to 0;
            delay(pause);//allows for motor to stop
            digitalWrite(12,HIGH);//HIGH for reverse
            delay(pause);//allows for relay to switch
            lm.write(spdpwmLeft);
            leftForward=false;
          }
        }
        else//forward
        {
          if(spdLeft>2.23)
            spdLeft=2.23;
          spdpwmLeft=spdLeft*170.0/2.2352;//brings it to a scale for pwm. max set at 170 to avoid any acidential overshoot
          Serial.println("Left forward");
          Serial.println(spdpwmLeft);
          if(spdpwmLeft==0)
          {
            lm.write(0);
            digitalWrite(5,LOW);
          }
          else
            digitalWrite(5,HIGH);
          if(leftForward==true)//if just changing speed
            lm.write(spdpwmLeft);
          else//if switching direction
          {
            digitalWrite(12,LOW);//LOW for leftForward
            delay(pause);//allows for relay to switch
            lm.write(spdpwmLeft);
            leftForward=true;
            Serial.println("Left forward");
            Serial.println(spdpwmLeft);
          }
        }//end of else if for left motor
        
        //right motor
        if (spdRight<0)//checks if Right is forward or reverse
        {
          spdRight=spdRight*-1;
          if(spdRight>2.23)
            spdRight=2.23;
          spdpwmRight=spdRight*170.0/2.2352;//brings it to a scale for pwm. max set at 170 to avoid any acidential overshoot
            Serial.println("Rightreverse");
            Serial.println(spdpwmRight);
          if(spdpwmRight==0)
          {
            rm.write(0);
            digitalWrite(7,LOW);
          }
          else
            digitalWrite(7,HIGH);
          if(rightForward!=true)//if just changing speed
            rm.write(spdpwmRight);
          else//if switching direction
          {
            rm.write(0);//sets pwm to 0;
            delay(pause);//allows for motor to stop
            digitalWrite(10,HIGH);//HIGH for reverse
            delay(pause);//allows for relay to switch
            rm.write(spdpwmRight);
            rightForward=false;
            Serial.println("Right reverse");
            Serial.println(spdpwmRight);
          }
        }
        else//forward
        {
          if(spdRight>2.23)
            spdRight=2.23;
          spdpwmRight=spdRight*170.0/2.2352;//brings it to a scale for pwm. max set at 170 to avoid any acidential overshoot
            Serial.println("Right forward");
            Serial.println(spdpwmRight);
          if(spdpwmRight==0)
          {
            rm.write(0);
            digitalWrite(7,LOW);
          }
          else
            digitalWrite(7,HIGH);
          if(rightForward==true)//if just changing speed
            rm.write(spdpwmRight);
          else//if switching direction
          {
            rm.write(0);//sets pwm to 0;
            digitalWrite(10,LOW);//LOW for RightForward
            delay(pause);//allows for relay to switch
            rm.write(spdpwmRight);
            rightForward=true;
            Serial.println("Right forward");
            Serial.println(spdpwmRight);
          }
        }//end of else if for Right motor
        
        setMessage("SETM");
    }
    
    
    
    
    
    
    //older controller style
    else if(CommandIdentifier == "LEFT" && digitalRead(8)==LOW)
      {
        if (lastCommand!=CommandIdentifier)
        {
          lm.write(0);
          rm.write(0);
          delay(pause);
        }
          
        //takes in the speed at which to turn
        int parameterIdentifierIndex = masterMessage.indexOf(':', CommandIdentifierIndex+1);
        if(parameterIdentifierIndex == -1) { return; Serial.println("Invalid parameter length");}
        
        Serial.println(parameterIdentifierIndex);
        String speedStr = masterMessage.substring(CommandIdentifierIndex+1, parameterIdentifierIndex);
        Serial.println(speedStr);
        
        spd=abs(speedStr.toFloat());
        if(spd>2.23)
          spd=2.23;
        spdpwm=spd*170.0/2.2352;//scales the value between 0 and 170
        digitalWrite(10,LOW);//right motor will go forward
        digitalWrite(12,HIGH);//left will go reverse
        delay(pause);
        rm.write(spdpwm);
        lm.write(spdpwm);
        lastCommand="LEFT";
        setMessage("LEFT");
      }
    else if(CommandIdentifier == "RIGHT" && digitalRead(8)==LOW)
      {
         if (lastCommand!=CommandIdentifier)
        {
          lm.write(0);
          rm.write(0);
          delay(pause);
        }
          
        //takes in the speed at which to turn
        int parameterIdentifierIndex = masterMessage.indexOf(':', CommandIdentifierIndex+1);
        if(parameterIdentifierIndex == -1) { return; Serial.println("Invalid parameter length");}
        
        Serial.println(parameterIdentifierIndex);
        String speedStr = masterMessage.substring(CommandIdentifierIndex+1, parameterIdentifierIndex);
        Serial.println(speedStr);
        
        spd=abs(speedStr.toFloat());
        if(spd>2.23)
          spd=2.23;
        spdpwm=spd*170.0/2.2352;//scales the value between 0 and 170
        digitalWrite(10,HIGH);//right motor will go reverse
        digitalWrite(12,LOW);//left will go forward
        delay(pause);
        rm.write(spdpwm);
        lm.write(spdpwm);
        lastCommand="RIGHT";
        setMessage("RIGHT");
      }
    else if(CommandIdentifier == "FORWARD" && digitalRead(8)==LOW)
      {
        digitalWrite(13,HIGH);
        if (lastCommand!=CommandIdentifier)
        {
          lm.write(0);
          rm.write(0);
          delay(pause);
        }
          
        //takes in the speed at which to turn
        int parameterIdentifierIndex = masterMessage.indexOf(':', CommandIdentifierIndex+1);
        if(parameterIdentifierIndex == -1) { return; Serial.println("Invalid parameter length");}
        
        Serial.println(parameterIdentifierIndex);
        String speedStr = masterMessage.substring(CommandIdentifierIndex+1, parameterIdentifierIndex);
        Serial.println(speedStr);
        
        spd=abs(speedStr.toFloat());
        if(spd>2.23)
          spd=2.23;
        spdpwm=spd*170.0/2.2352;//scales the value between 0 and 170
        digitalWrite(10,LOW);//right motor will go forward
        digitalWrite(12,LOW);//left will go forward
        delay(pause);
        rm.write(spdpwm);
        lm.write(spdpwm);
        lastCommand="FORWARD";
        setMessage("FORWARD");
      }
    else if(CommandIdentifier == "REVERSE" && digitalRead(8)==LOW)
      {
        digitalWrite(13,LOW);
        if (lastCommand!=CommandIdentifier)
        {
          lm.write(0);
          rm.write(0);
          delay(pause);
        }
          
        //takes in the speed at which to turn
        int parameterIdentifierIndex = masterMessage.indexOf(':', CommandIdentifierIndex+1);
        if(parameterIdentifierIndex == -1) { return; Serial.println("Invalid parameter length");}
        
        Serial.println(parameterIdentifierIndex);
        String speedStr = masterMessage.substring(CommandIdentifierIndex+1, parameterIdentifierIndex);
        Serial.println(speedStr);
        
        spd=abs(speedStr.toFloat());
        if(spd>2.23)
          spd=2.23;
        spdpwm=spd*170.0/2.2352;//scales the value between 0 and 170
        digitalWrite(10,HIGH);//right motor will go reverse
        digitalWrite(12,HIGH);//left will go reverse
        delay(pause);
        rm.write(spdpwm);
        lm.write(spdpwm);
        lastCommand="REVERSE";
        setMessage("REVERSE");
      }
    else if(CommandIdentifier == "STOP" && digitalRead(8)==LOW)
      {
        lm.write(0);
        rm.write(0);
        setMessage("STOP");
      }
    else if(CommandIdentifier == "SET SPEED")
    {
      int parameterIdentifierIndex = masterMessage.indexOf(':', CommandIdentifierIndex+1);
      if(parameterIdentifierIndex == -1) { return; Serial.println("Invalid parameter length");}
      
      Serial.println(parameterIdentifierIndex);
      String speedStr = masterMessage.substring(CommandIdentifierIndex+1, parameterIdentifierIndex);
      Serial.println(speedStr);
      if(digitalRead(8)==LOW)//pin 8 will be high if wireless estop is press.
      {
        double spd = speedStr.toFloat();  //takes the string and makes it an int
        int spdpwm;
        if (spd<0)//checks if rightForward or reverse
        {
          spd=spd*-1;
          if(spd>2.23)
            spd=2.23;
          spdpwm=spd*170.0/2.2352;//brings it to a scale for pwm. max set at 170 to avoid any acidential overshoot
            Serial.println(spdpwm);
          if(rightForward!=true)//if just changing speed
            rm.write(spdpwm);
          else//if switching direction
          {
            digitalWrite(9,0);
            delay(pause);//allows for relay to switch
            digitalWrite(10,HIGH);//HIGH for reverse
            rm.write(spdpwm);
            rightForward=false;
          }
        }
        else
        {
          if(spd>2.23)
            spd=2.23;
          spdpwm=spd*170.0/2.2352;//brings it to a scale for pwm. max set at 170 to avoid any acidential overshoot
          if(rightForward==true)//if just changing speed
            rm.write(spdpwm);
          else//if switching direction
          {
            digitalWrite(9,0);
            delay(pause);//allows for relay to switch
            digitalWrite(10,LOW);//LOW for rightForward
            rm.write(spdpwm);
            rightForward=true;
          }
          Serial.println(spdpwm);
        }
      }//end of if for wireless estop
      else{ rm.write(0); Serial.println("E-Stop triggered"); }//writes 0 to the motors if wireless estop is pressed
    }
    
    masterMessage = "";
  }
}

void setMessage(String message)
{
  int l = message.length();
  int i;
  for(i = 0; i < l; i++)
  {
    slaveMessage[i] = message[i];
  }
  while(i < messageSize)
  {
    slaveMessage[i] = ' ';
    i++;
  }
  
  slaveMessageReady = true;
}  

void receiveEvent(int howMany)
{
  masterMessage = "";
  for(int i = 0; i < howMany; i++)
  {
    masterMessage += (char)Wire.read();
  }
}

void requestEvent()
{
  if(slaveMessageReady)
  {
    String m = "";
    for(int i = 0; i < messageSize; i++)
    {
      m += slaveMessage[i];
    }
    slaveMessageReady = false;
    Wire.write(m.c_str());
  }
  else
  {
    String m = "";
    for(int i = 0; i < messageSize; i++)
    {
      m += emptyMessage[i];
    }
    Wire.write(m.c_str());
  }
}
