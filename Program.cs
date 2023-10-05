Console.WriteLine("Anti idle");

PowerHelper.ForceSystemAwake(); 

var lastMousePosition = MouseHelper.GetMouseCurrent();

const int SECONDS_DELAY = 25; 

while (true) {
    //Wait thread 
    Thread.Sleep(new TimeSpan(0,0,SECONDS_DELAY));

    //Check if current mouse position is the same that previous one, if so, then move the mouse to avoid idle
    var current = MouseHelper.GetMouseCurrent();

    if (current.x == lastMousePosition.x && current.y == lastMousePosition.y) {
        MouseHelper.MoveAndClick(0,0);
        Console.WriteLine("anti idle");
    }

    lastMousePosition = current;

}