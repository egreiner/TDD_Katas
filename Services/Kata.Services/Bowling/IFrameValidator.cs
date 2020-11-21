namespace Kata.Services.Bowling
{
    public interface IFrameValidator
    {
        void Validate(Frame frame, int pins);
    }
}