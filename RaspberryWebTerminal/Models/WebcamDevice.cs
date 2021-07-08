﻿using System;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;

namespace RaspberryWebTerminal.Models
{
    public class WebcamDevice
    {
        private FilterInfoCollection _videoDevices;
        private VideoCaptureDevice _device;
        private VideoCapabilities[] _snapshotCapabilities;
        public Bitmap LastFrame;

        public bool IsSucceeded { get; }
        public string What { get; }
        public string Camera { get; }
        public string DestinationAddress { get; set; }

        public WebcamDevice()
        {
            _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (_videoDevices.Count == 0)
            {
                What = "Unable to find any compatible camera device.";
                IsSucceeded = false;
                throw new ApplicationException(What);
            }

            Camera = _videoDevices[0].Name;
            _device = new VideoCaptureDevice(_videoDevices[0].MonikerString);
            _snapshotCapabilities = _device.SnapshotCapabilities;

            if (_snapshotCapabilities.Length == 0)
            {
                What = $"Camera {Camera} does not support capture control.";
                IsSucceeded = false;
                throw new ApplicationException(What);
            }

            _device.NewFrame += new NewFrameEventHandler(GetFrame);
            _device.Start();
            if (_device.IsRunning == false)
            {
                What = $"Unable to start camera {Camera}, operation aborted.";
                IsSucceeded = false;
                throw new ApplicationException(What);
            }
            
            IsSucceeded = true;
            What = "Webcam founded successfully.";
        }

        public void GetFrame(object sender, NewFrameEventArgs eventArgs)
        {
            LastFrame = eventArgs.Frame;
        }

        public Bitmap CaptureFrame()
        {
            return LastFrame;
        }

        public void GetSnapshot()
        {
            LastFrame.Save(DestinationAddress);
        }
        
        public void Shutdown()
        {
            _device.SignalToStop();
        }
    }
}