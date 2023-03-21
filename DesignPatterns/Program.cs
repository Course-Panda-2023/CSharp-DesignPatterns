
using DesignPatterns;

ControlUnit controlUnit = new ControlUnit(@"/Users/ilanmotiei/Desktop/army/DesignPatterns/DesignPatterns/data/3S.txt");
Customer customer = new Customer(@"/Users/ilanmotiei/Desktop/army/DesignPatterns/DesignPatterns/data/3R.txt", controlUnit);

Time.Attach(controlUnit);
Time.Attach(customer);

Time.Run(false);