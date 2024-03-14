

var drawCube = function()
{
    let A,B,C = 0;
    let x,y,z = 0;
    let cubeWidth = 10;
    let width = 160;
    let height = 44;
    let incrementSpeed = 0.6;
    const distancefromCam = 60;
    let outOfZBounds;
    let xp,yp;
    let index;
    let zBuffer = [160*44];
    let buffer = [160*44];
    function calculateX(i,j,k)
    {
        return j*Math.sin(A)*Math.sin(B)*Math.cos(C) - k*Math.cos(A)*Math.sin(B)*Math.cos(C) + j*Math.cos(A)*Math.sin(C) + k*Math.sin(A)*Math.sin(C) + i*Math.cos(B)*Math.cos(C);
    }
    function calculateY(i,j,k)
    {
        return j*Math.cos(A)*Math.cos(C) + k*Math.sin(A)*Math.cos(C) - j*Math.sin(A)*Math.sin(B)*Math.sin(C) + k*Math.cos(A)*Math.sin(B)*Math.sin(C) - i*Math.cos(B)*Math.sin(C);
    }
    function calculateZ(i,j,k)
    {
        return k*Math.cos(A)*Math.cos(B) - j*Math.sin(A)*Math.cos(B) + i*Math.sin(B);
    }
    function calculateSurface(cubeX,cubeY,cubeZ, char)
    {
        x = calculateX(cubeX,cubeY,cubeZ);
        y = calculateY(cubeX,cubeY,cubeZ);
        z = calculateZ(cubeX,cubeY,cubeZ) + distancefromCam;
        outOfZBounds = 1/z;

        xp = width/2 + K1*outOfZBounds*x*2;
        yp = height/2 + K1*outOfZBounds*y;

        index = xp + yp * width;
        if(index >= 0 && index < width*height)
        {
            if(outOfZBounds > zBuffer[index])
            {
                zBuffer[index] = outOfZBounds;
                buffer[index] = char;
            }
        }
    }
    return function main()
    {
        console.log("\x1b[2J");
        while(true){
            for (let cubeX = -cubeWidth; cubeX<cubeWidth; cubeX+= incrementSpeed)
                for (let cubeY = -cubeWidth; cubeY<cubeWidth; cubeY+= incrementSpeed)
                    calculateSurface(cubeX,cubeY,-cubeWidth,'#');
            console.log("\x1b[H");
            for(k=0; k<width*height; k++)
            {
                var c = (k % width ? buffer[k] : 10);
            }
            A += 0.005;
            B += 0.005;
        }
    }
}