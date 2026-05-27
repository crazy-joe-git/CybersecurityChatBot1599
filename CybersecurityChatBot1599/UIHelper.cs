using System;

namespace CybersecurityChatBot1599
{
    public static class UIHelper
    {
        //Displays the ASCII art after user enters their name.
        public static string GetHeaderBanner()
        {
            return @"============================================================
    ███████╗       ██████╗     ██████╗     ████████╗
    ██╔════╝       ██╔══██╗   ██      ██   ╚══██╔══╝
    █████╗  ████╗  ██████╔╝   ██      ██      ██║   
    ██╔══╝  ╚═══╝  ██╔══██╗   ██      ██      ██║   
    ███████╗       ██████╔╝    ██████╔╝       ██║   
    ╚══════╝       ╚═════╝     ╚═════╝        ╚═╝   
                                                                   
                        E-Bot
          CYBERSECURITY AWARENESS BOT
                    ============================================================
          Learn about staying safe online!
  Type 'exit' anytime to quit the program application.
                    ============================================================";
        }
    }
}