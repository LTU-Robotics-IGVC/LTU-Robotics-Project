#include <Wire.h>
#include <Servo.h>
//#include <LSM303.h>


//LSM303 compass;
Servo rm,lm;
int pause;//delay for switching direction
const int messageSize = 1;
String command = "";
double spd=1.0;
int spdpwm;
String lastCommand;
boolean autonomous;
boolean light;
int timer;

//left          L
//swerve left   Q
//swerve right  E
//right         R
//forward       F
//backwards     B
//stop          S
//Compass       C
//Autonomous    A

void setup()
{
  //rightmotor
  rm.attach(9);   //attach the signal pin of the ESC to pin 9
  rm.write(0);
  pinMode(10,OUTPUT);  //pin 10 will control the relay to flip the right motor
  digitalWrite(10,LOW);
  pinMode(7,OUTPUT);//controling right estop
  digitalWrite(7,HIGH);
  
  //leftmotor
  lm.attach(11);   //attach the signal pin of the ESC to pin 11
  lm.write(0);
  pinMode(12,OUTPUT);  //pin 12 will control the relay to flip the left motor
  digitalWrite(12,LOW);
  pinMode(5,OUTPUT);//controling left estop
  digitalWrite(5,HIGH);
  
  pause=50;//This worked when John and I tested it
  pinMode(8,INPUT);//wireless estop input
  pinMode(4,OUTPUT);//sets the output for the light
  digitalWrite(4,LOW);//sets pin to LOW turing on the light
  autonomous=false;
  light=true;
  timer=1;
  
  //setting up communication
  Serial.begin(9600);
  Wire.begin();
  //compass.init();
  //compass.enableDefault();
  
  //compass.m_min = (LSM303::vector<int16_t>){-2532, -3089, -4761};
  //compass.m_max = (LSM303::vector<int16_t>){3073, 2592, 2427};
  
  pinMode(13, OUTPUT);
  digitalWrite(13, HIGH);
}

void loop()
{
  //compass.read();
  //float Heading = compass.heading();
  //sendSerialInfo(String(Heading));
  //Serial.println(String(Heading));
  delay(100);
  if (digitalRead(8)==HIGH)//if wireless estop is pressed then motors will be set to 0
  {
    rm.write(0);
    lm.write(0);
  }
  Serial.println(timer);
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
  command = getSerialCommand();
  if(command != "")
  {
    String CommandIdentifier = command;
    Serial.println(CommandIdentifier);
    //Simple Code
    if(CommandIdentifier == "A")//make the light flash or not flash
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
    if(CommandIdentifier == "C")//Get the Compass
    {
      //compass.read();
      //float Heading = compass.heading();
      //sendSerialInfo(String(Heading));
      digitalWrite(13, HIGH);
      delay(20);
      digitalWrite(13, LOW);
      delay(20);
      digitalWrite(13, HIGH);
    }
    if(CommandIdentifier == "L" && digitalRead(8)==LOW)
      {
        if (lastCommand!=CommandIdentifier)
        {
          digitalWrite(5,HIGH);//turns off left estop
          digitalWrite(7,HIGH);//turns off right estop
          lm.write(0);
          rm.write(0);
          delay(pause);
        }

        if(spd>2.23)
          spd=2.23;
        spdpwm=spd*170.0/2.2352;//scales the value between 0 and 170
        digitalWrite(10,LOW);//right motor will go forward
        digitalWrite(5,LOW);//turns on left estop
        //digitalWrite(12,HIGH);//left will go reverse
        delay(pause);
        rm.write(spdpwm);
        //lm.write(spdpwm);
        lastCommand=CommandIdentifier;
      }
    else if(CommandIdentifier == "R" && digitalRead(8)==LOW)
      {
         if (lastCommand!=CommandIdentifier)
        {
          digitalWrite(5,HIGH);//turns off left estop
          digitalWrite(7,HIGH);//turns off right estop
          lm.write(0);
          rm.write(0);
          delay(pause);
        }

        if(spd>2.23)
          spd=2.23;
        spdpwm=spd*170.0/2.2352;//scales the value between 0 and 170
        //digitalWrite(10,HIGH);//right motor will go reverse
        digitalWrite(7,LOW);//turns on right estop
        digitalWrite(12,LOW);//left will go forward
        delay(pause);
        //rm.write(spdpwm);
        lm.write(spdpwm);
        lastCommand=CommandIdentifier;
      }
    else if(CommandIdentifier == "F" && digitalRead(8)==LOW)
      {
        digitalWrite(13,HIGH);
        Serial.println("Forward");
        if (lastCommand!=CommandIdentifier)
        {
          digitalWrite(5,HIGH);//turns off left estop
          digitalWrite(7,HIGH);//turns off right estop
          lm.write(0);
          rm.write(0);
          delay(pause);
        }
        if(spd>2.23)
          spd=2.23;
        spdpwm=spd*170.0/2.2352;//scales the value between 0 and 170
        digitalWrite(10,LOW);//right motor will go forward
        digitalWrite(12,LOW);//left will go forward
        delay(pause);
        rm.write(spdpwm);
        lm.write(spdpwm);
        lastCommand=CommandIdentifier;
      }
    else if(CommandIdentifier == "B" && digitalRead(8)==LOW)
      {
        digitalWrite(13,LOW);
        if (lastCommand!=CommandIdentifier)
        {
          lm.write(0);
          rm.write(0);
          delay(pause);
        }

        if(spd>2.23)
          spd=2.23;
        spdpwm=spd*170.0/2.2352;//scales the value between 0 and 170
        //digitalWrite(10,HIGH);//right motor will go reverse
        //digitalWrite(12,HIGH);//left will go reverse
        delay(pause);
        //rm.write(spdpwm);
        //lm.write(spdpwm);
        lastCommand=CommandIdentifier;
      }
    //swerving right
    else if(CommandIdentifier == "E" && digitalRead(8)==LOW)
    {
      if (lastCommand!=CommandIdentifier)
        {
          lm.write(0);
          rm.write(0);
          delay(pause);
        }
        spdpwm=spd*170.0/2.2352;//scales the value between 0 and 170
        digitalWrite(10,LOW);//right will go forward
        digitalWrite(12,LOW);//left will go forward
        delay(pause);
        rm.write(spdpwm);
        lm.write(0);
        lastCommand=CommandIdentifier;
    }
        //swerving left
    else if(CommandIdentifier == "Q" && digitalRead(8)==LOW)
    {
      if (lastCommand!=CommandIdentifier)
        {
          lm.write(0);
          rm.write(0);
          delay(pause);
        }
        spdpwm=spd*170.0/2.2352;//scales the value between 0 and 170
        digitalWrite(10,LOW);//right will go forward
        digitalWrite(12,LOW);//left will go forward
        delay(pause);
        rm.write(0);
        lm.write(spdpwm);
        lastCommand=CommandIdentifier;
    }
    else if(CommandIdentifier == "S" && digitalRead(8)==LOW)
      {
        lm.write(0);
        rm.write(0);
      }
    command="";
  }
}

String getSerialCommand()
{
  String m = "";
  if(Serial.available() < messageSize)
    return m;
    
  for(int i = 0; i < messageSize; i++)
  {
    m += (char)Serial.read();
  }
  Serial.println(m);
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
