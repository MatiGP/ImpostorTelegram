using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpostorTelegram
{
    class Constants
    {
        public const float USER_NAME_TABLE_HEIGHT = 10f;
        public const float USER_MESSAGE_TABLE_HEIGHT = 20f;
        public static Color SENT_MESSAGE_BACKGROUND_COLOR = Color.AliceBlue;
        public static Color RECEIVED_MESSAGE_BACKGROUND_COLOR = Color.Beige;
        public static Color FONT_COLOR = Color.White;
        public static Color SECONDARY_BACKGROUND_COLOR = Color.FromArgb(255, 38, 40, 102);
        public static Color HIGHLIGHT_BACKGROUND_COLOR = Color.FromArgb(255, 46, 156, 202);
        public static Color MAIN_BACKGROUND_COLOR = Color.FromArgb(255, 37, 39, 77);
        public static Font GLOBAL_NORMAL_FONT = new Font("Century Gothic", 14);
        public static Font GLOBAL_BIG_FONT = new Font("Century Gothic Bold", 30);
        public const string DEFAULT_LOBBY_NAME = "Lobby";
        public const string DEFAULT_LOBBY_EXCHANGE_NAME = "LobbyExch";
        public const int MESSAGE_BUTTON_HEIGHT = 80;
        public static Dictionary<EExchangeType, string> EXCHANGE_TYPES = new Dictionary<EExchangeType, string>()
        {
            { EExchangeType.Fanout, "fanout" },
            { EExchangeType.Direct, "direct" }
        };


    }

    public enum EExchangeType
    {
        Fanout,
        Direct
    }
}
