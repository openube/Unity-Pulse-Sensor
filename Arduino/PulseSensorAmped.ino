int pulsePin = 0;
int blinkPin = 13;

volatile int BPM;
volatile int Signal;
volatile int IBI = 600;
volatile boolean Pulse = false;
volatile boolean QS = false;

void setup()
{
  Serial.begin(9600);
  pinMode(blinkPin, OUTPUT);

  interruptSetup();
}

void loop()
{
  if (QS == true)
  {
    Serial.println(BPM);
    QS = false;
  }

  delay(20);
}
