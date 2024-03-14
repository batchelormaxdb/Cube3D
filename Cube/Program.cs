public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello :3");
        Cube cube = new Cube();
        Task drawCubeTask = new Task(() => cube.DrawCube());
        drawCubeTask.Start();

        Console.ReadKey();
    }
}
public class Cube
{

    float A, B, C;
    float x, y, z;
    float cubeWidth = 10;
    int width = 160;
    int height = 44;
    float incrementSpeed = 0.6f;
    int distancefromCam = 60;
    float outOfZBounds;
    float horizontalOffset;
    int xp, yp;
    int index;
    float K1 = 40;
    int backGroundASCIICode = '.';
    List<int> zBuffer = new List<int>(new int[160 * 44]);
    List<int> buffer = new List<int>(new int[160 * 44]);
    float calculateX(int i, int j, int k)
    {
        return j * MathF.Sin(A) * MathF.Sin(B) * MathF.Cos(C) - k * MathF.Cos(A) * MathF.Sin(B) * MathF.Cos(C) + j * MathF.Cos(A) * MathF.Sin(C) + k * MathF.Sin(A) * MathF.Sin(C) + i * MathF.Cos(B) * MathF.Cos(C);

    }
    float calculateY(int i, int j, int k)
    {
        return j * MathF.Cos(A) * MathF.Cos(C) + k * MathF.Sin(A) * MathF.Cos(C) - j * MathF.Sin(A) * MathF.Sin(B) * MathF.Sin(C) + k * MathF.Cos(A) * MathF.Sin(B) * MathF.Sin(C) - i * MathF.Cos(B) * MathF.Sin(C);
    }
    float calculateZ(int i, int j, int k)
    {
        return k * MathF.Cos(A) * MathF.Cos(B) - j * MathF.Sin(A) * MathF.Cos(B) + i * MathF.Sin(B);
    }
    void calculateSurface(float cubeX, float cubeY, float cubeZ, int ch)
    {
        x = calculateX((int)cubeX, (int)cubeY, (int)cubeZ);
        y = calculateY((int)cubeX, (int)cubeY, (int)cubeZ);
        z = calculateZ((int)cubeX, (int)cubeY, (int)cubeZ) + distancefromCam;
        outOfZBounds = 1 / z;

        xp = (int)(width / 2 + K1 * outOfZBounds * x * 2);
        yp = (int)(height / 2 + K1 * outOfZBounds * y);

        index = xp + yp * width;
        if (index >= 0 && index < width * height)
        {

            if (outOfZBounds > zBuffer[index])
            {
                zBuffer[index] = (int)outOfZBounds;
                buffer[index] = ch;
            }
        }
    }
    public async Task DrawCube()
    {
        while (true)
        {
            for (int i = 0; i < height * width; i++)
            {
                buffer[i] = backGroundASCIICode;
            }
            cubeWidth = 15;
            zBuffer = new List<int>(new int[height * width * 4]);

            for (float cubeX = -cubeWidth; cubeX < cubeWidth; cubeX += incrementSpeed)
            {
                for (float cubeY = -cubeWidth; cubeY < cubeWidth; cubeY += incrementSpeed)
                {
                    calculateSurface(cubeX, cubeY, -cubeWidth, '.');
                    calculateSurface(cubeWidth, cubeY, cubeX, '$');
                    calculateSurface(-cubeWidth, cubeY, -cubeX, '~');
                    calculateSurface(-cubeX, cubeY, cubeWidth, '#');
                    calculateSurface(cubeX, -cubeWidth, -cubeY, '&');
                    calculateSurface(cubeX, cubeWidth, cubeY, '+');

                }
            }
            char[] consoleStream = new char[width * height];
            for (int k = 0; k < width * height; k++)
            {
                consoleStream[k] = (k % width) != 0 ? ((char)buffer[k]) : ((char)10);
            }
            for (int i = 0; i < height; i++)
            {
                Console.Write('\n');
            }
            Console.WriteLine(consoleStream);
            A += 0.005f;
            B += 0.005f;

            await Task.Delay(1);
        }
    }
}