using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WateryTart.MusicAssistant.Messages
{
    public static partial class Commands
    {
        public static string PlayerQueuesAll = "player_queues/all";
        public static string PlayerQueuesClear = "player_queues/clear";
        public static string PlayerQueuesDeleteItem = "player_queues/delete_item";
        public static string PlayerQueuesDontStopTheMusic = "player_queues/dont_stop_the_music";
        public static string PlayerQueuesGet = "player_queues/get";
        public static string PlayerQueuesGetActiveQueue = "player_queues/get_active_queue";
        public static string PlayerQueuesItems = "player_queues/items";
        public static string PlayerQueuesMoveItem = "player_queues/move_item";
        public static string PlayerQueuesNext = "player_queues/next";
        public static string PlayerQueuesPause = "player_queues/pause";
        public static string PlayerQueuesPlay = "player_queues/play";
        public static string PlayerQueuesPlayIndex = "player_queues/play_index";
        public static string PlayerQueuesPlayMedia = "player_queues/play_media";
        public static string PlayerQueuesPlayPause = "player_queues/play_pause";
        public static string PlayerQueuesPrevious = "player_queues/previous";
        public static string PlayerQueuesRepeat = "player_queues/repeat";
        public static string PlayerQueuesResume = "player_queues/resume";
        public static string PlayerQueuesSeek = "player_queues/seek";
        public static string PlayerQueuesShuffle = "player_queues/shuffle";
        public static string PlayerQueuesSkip = "player_queues/skip";
        public static string PlayerQueuesStop = "player_queues/stop";
        public static string PlayerQueuesTransfer = "player_queues/transfer";
    }
}