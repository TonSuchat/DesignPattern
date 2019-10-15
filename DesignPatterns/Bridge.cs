using System;

namespace DesignPattern.DesignPatterns
{
    public class Bridge : Pattern
    {
        // Abstraction
        public class Remote
        {
            private readonly IDevice _device;
            public Remote(IDevice device) => _device = device;

            public void TogglePower()
            {
                if (_device.IsActive) _device.TurnOff();
                else _device.TurnOn();
            }
            public void IncreaseVolumn() => _device.SetVolumn(_device.Volumn + 1);
            public void DecreaseVolumn() => _device.SetVolumn(_device.Volumn - 1);
            public void IncreaseChannel() => _device.SetChannel(_device.Channel + 1);
            public void DecreaseChannel() => _device.SetChannel(_device.Channel - 1);
        }

        // Implementation
        public interface IDevice
        {
            bool IsActive { get; set; }
            int Channel { get; set; }
            int Volumn { get; set; }
            void TurnOn();
            void TurnOff();
            int GetVolumn();
            void SetVolumn(int volumn);
            int GetChannel();
            void SetChannel(int channel);
        }

        // concrete implementation
        public class Television : IDevice
        {
            public bool IsActive { get; set; }
            public int Channel { get; set; }
            public int Volumn { get; set; }
            public Television()
            {
                IsActive = false;
                Channel = 1;
                Volumn = 1;
            }

            public int GetChannel() => Channel;

            public int GetVolumn() => Volumn;

            public void SetChannel(int channel) => Channel = channel;

            public void SetVolumn(int volumn) => Volumn = volumn;

            public void TurnOff() => IsActive = false;

            public void TurnOn() => IsActive = true;
        }

        // client
        /// <summary>
        /// Problem: When we have 2 independent and we need to working with both of them
        /// Solved: We changed from inheritance to composition use Bridge pattern for connect both of them
        /// </summary>
        public override void Demo()
        {
            IDevice television = new Television();
            Remote remote = new Remote(television);
            Console.WriteLine($"Turned on: {television.IsActive}, Volumn: {television.Volumn}, Channel: {television.Channel}");
            remote.TogglePower();
            remote.IncreaseChannel();
            remote.IncreaseVolumn();
            Console.WriteLine($"Turned on: {television.IsActive}, Volumn: {television.Volumn}, Channel: {television.Channel}");
        }

    }
}
