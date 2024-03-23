package Objects;

import javax.imageio.ImageIO;
import java.awt.*;
import java.io.IOException;
import java.util.Random;
import Main.GamePanel;
public class Tree extends SuperObject{

    public Tree(int worldWidth,int worldHeight,int tileSize){
        name="tree";
        Random rand=new Random();
        try{
            image = ImageIO.read(getClass().getResourceAsStream("/Objects/bigtree.png"));
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
    GamePanel gp;
    public Rectangle getCollisionArea() {
        // Calculate and return the collision area of the tree object
        return new Rectangle(worldX, worldY,gp.worldWidth ,gp.worldHeight);
    }

}
