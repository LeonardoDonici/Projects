package Enemy;

import Entity.Entity;
import Main.GamePanel;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.util.Random;

public class WalkingGoblin extends Entity {

    public WalkingGoblin(GamePanel gp) {
        super(gp);

        type=1;
        direction="down";
        speed=1;
        getPlayerImage();
    }

    private void getPlayerImage() {


        try {
            BufferedImage spriteSheet = ImageIO.read(getClass().getResourceAsStream("/Enemy/Walkinggoblin.png"));

            int spriteWidth = gp.originalTileSize;
            int spriteHeight = gp.originalTileSize;

            down1 = spriteSheet.getSubimage(0, 0, spriteWidth, spriteHeight);
            down2 = spriteSheet.getSubimage(spriteWidth, 0, spriteWidth, spriteHeight);
            left1 = spriteSheet.getSubimage(2 * spriteWidth, 0, spriteWidth, spriteHeight);
            left2= spriteSheet.getSubimage(3 * spriteWidth, 0, spriteWidth, spriteHeight);
            right1 = spriteSheet.getSubimage(4 * spriteWidth, 0, spriteWidth, spriteHeight);
            right2 = spriteSheet.getSubimage(5 * spriteWidth, 0, spriteWidth, spriteHeight);
            up1 = spriteSheet.getSubimage(6 * spriteWidth, 0, spriteWidth, spriteHeight);
            up2 = spriteSheet.getSubimage(7 * spriteWidth, 0, spriteWidth, spriteHeight);

        } catch(IOException e) {
            e.printStackTrace();

        }
    }

    public void update() {


        super.update();

        int xDistance = Math.abs(worldX - gp.player.worldX);
        int yDistance = Math.abs(worldY - gp.player.worldY);
        int tileDistance = (xDistance + yDistance)/gp.tileSize;

        if (tileDistance < 5) {

            int i = new Random().nextInt(100) + 1;
            if(i > 50) {
                onPath = true;
                speed = 2; // Set speed to 3 if there is line of sight
            }

        }
        if (onPath == true && tileDistance > 5) {
            onPath = false;
            speed = 1; // Reset speed to default value
        }
    }


    public void setAction() {

        if (onPath == true) {
            int goalCol = (gp.player.worldX + gp.player.solidArea.x) / gp.tileSize;
            int goalRow = (gp.player.worldY + gp.player.solidArea.y) / gp.tileSize;
            searchPath(goalCol, goalRow, 1);
        } else {

            actionLockCounter++;
            if (actionLockCounter == 120) {
                Random random = new Random();
                int i = random.nextInt(100) + 1;

                if (i <= 25) {
                    direction = "up";
                }

                if (i > 25 && i <= 50) {
                    direction = "down";
                }

                if (i > 50 && i <= 75) {
                    direction = "left";
                }

                if (i > 75 && i <= 100) {
                    direction = "right";
                }
                actionLockCounter = 0;

            }
        }
    }

}
