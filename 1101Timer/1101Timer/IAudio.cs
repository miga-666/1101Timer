using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Media;
using Xamarin.Forms;

namespace _1101Timer
{
    // 創建 IAudioService 接口
        public interface IAudio
    {
        // 有播放音樂的方法
        void PlayAudioFile(string fileName);
        void StopAudioFile();

    }
    // 實現接口
    public class AudioRender : IAudio
    {
        MediaPlayer player;
        public void PlayAudioFile(string fileName)
        {
            player = new MediaPlayer();
            var file = global::Android.App.Application.Context.Assets.OpenFd(fileName);
            // SetDataSource(): 將數據源設置為內容 Uri
            // Sfile.FileDescriptor: 文件描述符
            // file.StartOffset: 要播放的數據開始的文件中的偏移量，以字節為單位
            // file.Length: 要播放的數據的字節長度
            player.SetDataSource(file.FileDescriptor, file.StartOffset, file.Length);
            // Prepared(): 同步準備播放器進行播放
            player.Prepare();
            // 開始播放音訊\
            player.Start();
        }
        public void StopAudioFile()
        {
            // Prepared(): 同步準備播放器進行播放
            player.Stop();
            // 開始播放音訊\
            player.Release();
        }
    }
}