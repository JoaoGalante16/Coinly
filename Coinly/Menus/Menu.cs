using System;
using System.Collections.Generic;
using System.Text;

namespace Coinly.Menus;

internal abstract class Menu
{ 
    public virtual async Task Executar()
    {
        Console.Clear();
        Console.WriteLine("   ____      _       _\r\n  / ___|___ (_)_ __ | |_   _\r\n | |   / _ \\| | '_ \\| | | | |\r\n | |__| (_) | | | | | | |_| |\r\n  \\____\\___/|_|_| |_|_|\\__, |\r\n                       |___/\n");
    }
}
 

