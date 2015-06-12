#include <LSM303.h>
#include <Wire.h>
#include <Servo.h>

Servo rm, lm;
boolean rightForward;
boolean leftForward;
boolean autonomous;
boolean light;
int pause = 50;
int timer;
const int messageSize = 30;
LSM303 compass;

void setup()
{
  rm.attach(9);
  rm.write(0);
  pinMode(10, OUTPUT);
  digitalWrite(13, LOW);
  rightForward = true;
  pinMode(7, OUTPUT);
  digitalWrite(7, HIGH);
  
  lm.attach(11);
  lm.write(0);
  pinMode(12, OUTPUT);
  digitalWrite(12, LOW);
  leftForward = true;
  pinMode(5, OUTPUT);
  digitalWrite(5, HIGH);
  
  pinMode(8, INPUT);
  
  autonomous = false;
  light=true;
  timer=1;
  pinMode(4, OUTPUT);
  digitalWrite(4, LOW);
  
  Serial.begin(9600);
  //Wire.begin();
  //compass.init();
  //compass.enableDefault();
  
  //compass.m_min = (LSM303::vector<int16_t>){-2532, -3089, -4761};
  //compass.m_max = (LSM303::vector<int16_t>){3073, 2592, 2427};
  pinMode(13, OUTPUT);
  digitalWrite(13, HIGH);
}

void loop()
{
  delay(20);
  if(autonomous && millis() % 1500 < 750) digitalWrite(4, HIGH);
  else digitalWrite(4, LOW);
  
  if(digitalRead(8) == HIGH)
  {
    rm.write(0);
    lm.write(0);
  }
  
  String command;
  while((command = getSerialCommand()) != "")
  {
    int CommandIdentifierIndex = command.indexOf(':');
    if(CommandIdentifierIndex == -1)return;
    
    String CommandIdentifier = command.substring(0, CommandIdentifierIndex);
    if(CommandIdentifier == "LED ON") { digitalWrite(13, HIGH);}
    else if(CommandIdentifier == "LED OFF") { digitalWrite(13, LOW);}
    else if(CommandIdentifier == "AUTO MODE")
    {
      autonomous=true;
    }
    else if(CommandIdentifier == "MANUAL MODE")
    {
      autonomous=false;
    }
    else if(CommandIdentifier == "SETM" && digitalRead(8)==LOW)
    {
      //grabs left motor speed
        int parameterIdentifierIndex = command.indexOf(':', CommandIdentifierIndex+1);
        if(parameterIdentifierIndex == -1) { return; }
        
        String speedStrLeft = command.substring(CommandIdentifierIndex+1, parameterIdentifierIndex);
        
        //grabs right motor speed
        int parameterIdentifierIndex2 = command.indexOf(':',parameterIdentifierIndex+1);
        if(parameterIdentifierIndex2==-1) { return; }
        
        String speedStrRight = command.substring(parameterIdentifierIndex+1, parameterIdentifierIndex2);
        
        
        double spdLeft = speedStrLeft.toFloat();  //takes the string and makes it an int
        int spdpwmLeft;
        double spdRight = speedStrRight.toFloat();  //takes the string and makes it an int
        int spdpwmRight;
        
        //left motor
        if (spdLeft<0)//checks if Left is forward or reverse
        {
          spdLeft=spdLeft*-1;
          if(spdLeft>2.23)
            spdLeft=2.23;
          spdpwmLeft=spdLeft*170.0/2.2352;//brings it to a scale for pwm. max set at 170 to avoid any acidential overshoot
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
          }
        }//end of else if for left motor
        
        //right motor
        if (spdRight<0)//checks if Right is forward or reverse
        {
          spdRight=spdRight*-1;
          if(spdRight>2.23)
            spdRight=2.23;
          spdpwmRight=spdRight*170.0/2.2352;//brings it to a scale for pwm. max set at 170 to avoid any acidential overshoot
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
          }
        }
        else//forward
        {
          if(spdRight>2.23)
            spdRight=2.23;
          spdpwmRight=spdRight*170.0/2.2352;//brings it to a scale for pwm. max set at 170 to avoid any acidential overshoot
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
          }
        }//end of else if for Right motor
    }//End of SETM ---------------------------------------------------------------------------------------------
    else if(CommandIdentifier == "COMPASS")
    {
      //compass.read();
      //float Heading = compass.heading();
      float Heading = 1000;
      sendSerialInfo("Compass:" + String(Heading));
    }
  }
}

String getSerialCommand()
{
  String m = "";
  if(Serial.available() < messageSize+2)
    return m;
  while(Serial.peek() != '*')
    Serial.read();
  Serial.read();
  for(int i = 0; i < messageSize; i++)
  {
    m += (char)Serial.read();
  }
  
  while(Serial.peek() != '\n')
    Serial.read();
  Serial.read();
  
  return m;
}
void sendSerialInfo(String info)
{
  int i = 0;
  int length = info.length();
  for(i = 0; i < messageSize && i < length; i++)
  {
    Serial.print(info[i]);
  }
  while(i < messageSize-1)
  {
    Serial.print(' ');
    i++;
  }
  Serial.println();
}
