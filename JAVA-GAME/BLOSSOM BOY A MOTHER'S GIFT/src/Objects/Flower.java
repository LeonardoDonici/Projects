package Objects;
import Main.AssetSetter;
import Main.GamePanel;

import javax.imageio.ImageIO;
import java.io.IOException;
import java.util.Random;


public class Flower extends SuperObject {


    public Flower(){
            name="flower";

        try {
            image = ImageIO.read(getClass().getResourceAsStream("/Objects/flower (2).png"));

        }catch (IOException e) {
            e.printStackTrace();
        }
    }

    public Flower(int randomX, int randomY, int tileSize) {



        name = "flower";

        try {
            image = ImageIO.read(getClass().getResourceAsStream("/Objects/flower (2).png"));

            worldX=randomX;
            worldY=randomY;
            //copaci

        }catch (IOException e) {
            e.printStackTrace();
        }
    }



}




