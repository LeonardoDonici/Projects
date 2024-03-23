package Objects;

import Entity.Projectiles;
import Main.GamePanel;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.IOException;


public class Bullet extends Projectiles {
    GamePanel gp;
    public Bullet(GamePanel gp){
        super(gp);
        this.gp=gp;
        name="Bullet";
        speed=2;
        maxLife=120;
        life=maxLife;
        attack=2;
        useCost=1;
        alive=false;
        getImage();
    }
    public void getImage(){
        try {
            BufferedImage enemyProjectile = ImageIO.read(getClass().getResourceAsStream("/Bullet/Bullet.png"));

            int spriteWidth = gp.originalTileSize;
            int spriteHeight = gp.originalTileSize;

            up1 = enemyProjectile.getSubimage(0, 0, spriteWidth, spriteHeight);
            up2 = enemyProjectile.getSubimage(0, 0, spriteWidth, spriteHeight);
            down1 = enemyProjectile.getSubimage( 0, 0, spriteWidth, spriteHeight);
            down2 = enemyProjectile.getSubimage(0, 0, spriteWidth, spriteHeight);
            right1 = enemyProjectile.getSubimage(0, 0, spriteWidth, spriteHeight);
            right2 = enemyProjectile.getSubimage(0, 0, spriteWidth, spriteHeight);
            left1 = enemyProjectile.getSubimage(0, 0, spriteWidth, spriteHeight);
            left2 = enemyProjectile.getSubimage(0, 0, spriteWidth, spriteHeight);

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

}
