package Objects;

import javax.imageio.ImageIO;
import java.io.IOException;
import java.util.Random;

public class Tree1 extends SuperObject {

    public Tree1(int worldWidth,int worldHeight,int tileSize){
        name="tree1";
        Random rand=new Random();
        try{
            image = ImageIO.read(getClass().getResourceAsStream("/Objects/tree.png"));
            int randomX = rand.nextInt((worldWidth-tileSize) / tileSize) * tileSize;
            int randomY = rand.nextInt((worldHeight-tileSize) / tileSize) * tileSize;
            if(randomX <= tileSize) randomX = randomX + (tileSize- randomX);
            if(randomY <= tileSize) randomY = randomY + (tileSize - randomY);
            worldX=randomX;
            worldY=randomY;
        }catch (IOException e){
            e.printStackTrace();
        }
        collision=true;
    }
}
