using SatelliteControl;

string fileBase = "C:\\Users\\User\\Desktop\\SatelliteEx\\data\\";
Customer c = new Customer(fileBase + "3R.txt");
ControlUnit cu = new ControlUnit(c, fileBase + "3S.txt");

cu.SatelliteCommunication();

Console.ReadLine();