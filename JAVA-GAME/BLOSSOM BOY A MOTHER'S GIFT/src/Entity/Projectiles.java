package Entity;

import Main.GamePanel;

import java.awt.*;
import java.awt.image.BufferedImage;

public class Projectiles extends Entity{
        Entity user;
        public Projectiles(GamePanel gp){
            super(gp);
        }
        public void set(int worldX, int worldY, String direction,boolean alive,Entity user){

                this.worldX=worldX;
                this.worldY=worldY;
                this.direction=direction;
                this.alive=alive;
                this.user=user;
                this.life=this.maxLife;
        }
        public void update(){

           if(user!= gp.player) {

               boolean contactPlayer = gp.cChecker.checkPlayer(this);
               if (contactPlayer == true) {
                   gp.gameState=gp.dialogueState;
                   gp.ui.showMessage("\n Ouch! A Goblin's bullet finds its mark, striking you with precision. The pain is temporary,\n but the lessons are enduring. Use this moment to recalibrate your approach, anticipate their attacks,\n and hone your evasion skills. Remember, setbacks are opportunities for growth. Rise above this setback\n fortify your resolve, and continue your quest with unwavering determination. Victory\n awaits those who learn from their wounds and press forward\n                  PRESS R TO RESTART                PRESS M FOR MAIN MENU");
               }
               if(gp.currentMap==2) {
                   super.update();
                   int xDistance = Math.abs(worldX - gp.player.worldX);
                   int yDistance = Math.abs(worldY - gp.player.worldY);
                   int tileDistance = (xDistance + yDistance) / gp.tileSize;

                   if (onPath == false && tileDistance < 5) {
                       onPath = true;
                       speed=1;
                   }
               }
           }



            collisionOn = false;
            gp.cChecker.checkTile(this);

            if(collisionOn == false) {
                switch (direction) {
                    case "up":
                        worldY -= speed;
                        break;
                    case "down":
                        worldY += speed;
                        break;
                    case "left":
                        worldX -= speed;
                        break;
                    case "right":
                        worldX += speed;
                        break;
                }
            }

           life--;
           if(life<=0){
                   alive=false;
           }
           spriteCounter++;
           if(spriteCounter>12){
                   if(spriteNum==1){
                           spriteNum=2;
                   }else if(spriteNum==2){
                           spriteNum=1;
                   }
                   spriteCounter=0;
           }

        }

    public void draw(Graphics2D g2) {

        int screenX = worldX - gp.player.worldX + gp.player.screenX;
        int screenY = worldY - gp.player.worldY + gp.player.screenY;
        BufferedImage image = null;

        switch (direction) {
            case "up":
                if (spriteNum == 1) {
                    image = up1;
                }
                if (spriteNum == 2) {
                    image = up2;
                }
                break;
            case "down":
                if (spriteNum == 1) {
                    image = down1;
                }
                if (spriteNum == 2) {
                    image = down2;
                }
                break;
            case "left":
                if (spriteNum == 1) {
                    image = left1;
                }
                if (spriteNum == 2) {
                    image = left2;
                }
                break;
            case "right":
                if (spriteNum == 1) {
                    image = right1;
                }
                if (spriteNum == 2) {
                    image = right2;
                }
        }

        g2.drawImage(image, screenX, screenY, gp.tileSize, gp.tileSize, null);
    }
    public void setAction() {

        if (onPath == true) {

            int goalCol = (gp.player.worldX + gp.player.solidArea.x) / gp.tileSize;
            int goalRow = (gp.player.worldY + gp.player.solidArea.y) / gp.tileSize;

            searchPath(goalCol, goalRow,1);
        }
    }
}




