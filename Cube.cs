using Math;
using stdio;


float A, B, C;
int x, y, z = 0;
float cubeWidth = 10;
float width = 160;
float height = 44;
float incrementSpeed = 0.6;
float distancefromCam = 60;
int outOfZBounds;
int xp, yp;
int index;
float zBuffer = [160 * 44];
char buffer = [160 * 44];
float calculateX(int i, int j, int k)
{
    return j * Math.sin(A) * Mathf.sin(B) * Math.cos(C) - k * Math.cos(A) * Math.sin(B) * Math.cos(C) + j * Math.cos(A) * Math.sin(C) + k * Math.sin(A) * Math.sin(C) + i * Math.cos(B) * Math.cos(C);
    
}
float calculateY(int i, int j, int k)
{
    return j * Math.cos(A) * Math.cos(C) + k * Math.sin(A) * Math.cos(C) - j * Math.sin(A) * Math.sin(B) * Math.sin(C) + k * Math.cos(A) * Math.sin(B) * Math.sin(C) - i * Math.cos(B) * Math.sin(C);
}
float calculateZ(i, j, k)
{
    return k * Math.cos(A) * Math.cos(B) - j * Math.sin(A) * Math.cos(B) + i * Math.sin(B);
}
float calculateSurface(float cubeX, float cubeY, float cubeZ, char)
{
    x = calculateX(cubeX, cubeY, cubeZ);
    y = calculateY(cubeX, cubeY, cubeZ);
    z = calculateZ(cubeX, cubeY, cubeZ) + distancefromCam;
    outOfZBounds = 1 / z;

    xp = width / 2 + K1 * outOfZBounds * x * 2;
    yp = height / 2 + K1 * outOfZBounds * y;

    index = xp + yp * width;
    if (index >= 0 && index < width * height)
    {
        if (outOfZBounds > zBuffer[index])
        {
            zBuffer[index] = outOfZBounds;
            buffer[index] = char;
        }
    }
}
int main()
{
    print("\x1b[2J");
    while (true)
    {
        for (let cubeX = -cubeWidth; cubeX < cubeWidth; cubeX += incrementSpeed)
            for (let cubeY = -cubeWidth; cubeY < cubeWidth; cubeY += incrementSpeed)
                calculateSurface(cubeX, cubeY, -cubeWidth, '#');
        console.log("\x1b[H");
        for (k = 0; k < width * height; k++)
        {
            var c = (k % width ? buffer[k] : 10);
        }
        A += 0.005;
        B += 0.005;
    }
}
}