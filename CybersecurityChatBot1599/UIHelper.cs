using System;

namespace CybersecurityChatBot1599
{
    public static class UIHelper
    {
        // Keeps ASCII banner decoupled from the view logic
        public static string GetHeaderBanner()
        {
            return @"============================================================
    ███████╗       ██████╗     ██████╗   ████████╗
    ██╔════╝       ██╔══██╗   ██     ██  ╚══██╔══╝
    █████╗  ████╗  ██████╔╝   ██     ██     ██║   
    ██╔══╝  ╚═══╝  ██╔══██╗   ██     ██     ██║   
    ███████╗       ██████╔╝    ██████╔╝     ██║   
    ╚══════╝       ╚═════╝     ╚═════╝      ╚═╝   
                                            
                    E-Bot
         CYBERSECURITY AWARENESS BOT
============================================================
 Learn about staying safe online!
 Type 'exit' anytime to quit the program application.
============================================================
";
        }
    }
}