package Entity;

import Main.GamePanel;
import Objects.SuperObject;
import Objects.Tree;

import java.awt.*;
import java.awt.image.BufferedImage;

public class Entity {
    public GamePanel gp;
    // VARIABILE PENTRU POZITIONAREA ENTITATILOR
    public int worldX, worldY;
    public int speed;

    // VARIABILE PENTRU IMAGINILE ENTITATILOR
    public BufferedImage up1, up2, down1, down2, left1, left2, right1, right2;
    public String direction;
    public int spriteCounter = 0;
    public int spriteNum = 1;

    // VARIABILE PENTRU DASH-UL ENTITATILOR
    public int dashCounter = 0;
    public int dashDuration = 4;
    public boolean isDashing = false;
    public int dashCooldown;

    // VARIABILE PENTRU COLIZIUNEA ENTITATILOR CU PERETI SI OBIECTE
    public Rectangle solidArea=new Rectangle(0,0,48,48);
    public boolean collisionOn=false;
    public int solidAreaDefaultX, solidAreaDefaultY;

    public boolean onPath = false;

    public boolean ciocnire = false;
    public int actionLockCounter=0;
    public int type;
    public String name;
    public int maxLife;
    public int life;
    public int attack;
    public int maxMana;
    public int mana;
    public boolean alive=true;
    public boolean isShot = false;

    public Projectiles projectile;
    public int useCost;


    public Entity(GamePanel gp) {
        this.gp=gp;

    }

    public void setAction(){

    }

    public void checkCollision() {

        collisionOn = false;
        gp.cChecker.checkTile(this);
        gp.cChecker.checkObject(this, false);
        boolean contactPlayer = gp.cChecker.checkPlayer(this);

        if((this.type == 1) && contactPlayer == true) {
            gp.gameState=gp.dialogueState;

            gp.ui.showMessage("\nAlas! The Goblins have finally caught up with you, ensnaring you in their clutches.Despite your valiant\n efforts, they proved too formidable this time. Do not despair, brave adventurer, for setbacks are mere \nstepping stones on the path to victory. Gather your strength, learn from this encounter and rise again with \nunwavering determination.Your journey is not over yet.Dust yourself off, and go ahead towards triumph!\n\n          PRESS R TO RESTART                   PRESS M FOR MAIN MENU");
        }
    }


    public void update(){
        setAction();

        checkCollision();

        if(collisionOn==false){

            switch (direction){
                case "up":
                    worldY -= speed;
                    break;
                case "down":
                    worldY += speed;
                    break;
                case"left":
                    worldX -= speed;
                    break;
                case "right":
                    worldX += speed;
                    break;
            }
        }

        spriteCounter++;
        if (spriteCounter > 10) {
            if (spriteNum == 1) {
                spriteNum = 2;
            } else if (spriteNum == 2) {
                spriteNum = 1;
            }
            spriteCounter = 0;
        }
    }
    public void draw(Graphics2D g2) {
        BufferedImage image=null;
        int screenX = worldX - gp.player.worldX + gp.player.screenX;
        int screenY = worldY - gp.player.worldY + gp.player.screenY;

        if(worldX + gp.tileSize > gp.player.worldX - gp.player.screenX && worldX - gp.tileSize < gp.player.worldX + gp.player.screenX && worldY + gp.tileSize > gp.player.worldY - gp.player.screenY && worldY - gp.tileSize < gp.player.worldY + gp.player.screenY) {

            switch(direction) {
                case "up":
                    if(spriteNum == 1){
                        image = up1;
                    }
                    if(spriteNum == 2) {
                        image = up2;
                    }
                    break;
                case "down":
                    if(spriteNum == 1) {
                        image = down1;
                    }
                    if(spriteNum == 2){
                        image = down2;
                    }
                    break;
                case "left":
                    if(spriteNum == 1) {
                        image = left1;
                    }
                    if(spriteNum == 2) {
                        image = left2;
                    }
                    break;
                case "right":
                    if(spriteNum == 1) {
                        image = right1;
                    }
                    if(spriteNum == 2) {
                        image = right2;
                    }
                    break;
            }

            g2.drawImage(image, screenX, screenY, gp.tileSize, gp.tileSize, null);

        }
    }

    public void searchPath(int goalCol, int goalRow, int speed) {
        int startCol = (worldX + solidArea.x) / gp.tileSize;
        int startRow = (worldY + solidArea.y) / gp.tileSize;

        gp.pFinder.setNodes(startCol, startRow, goalCol, goalRow, this);

        if (gp.pFinder.search()) {
            int nextCol = gp.pFinder.pathList.get(0).col;
            int nextRow = gp.pFinder.pathList.get(0).row;

            int nextX = nextCol * gp.tileSize;
            int nextY = nextRow * gp.tileSize;

            int enLeftX = worldX + solidArea.x;
            int enTopY = worldY + solidArea.y;

            int horizontalMovement = nextX - enLeftX;
            int verticalMovement = nextY - enTopY;

            if (horizontalMovement < 0) {
                direction = "left";
            } else if (horizontalMovement > 0) {
                direction = "right";
            } else if (verticalMovement < 0) {
                direction = "up";
            } else if (verticalMovement > 0) {
                direction = "down";
            }

            int dx = 0;
            int dy = 0;

            if (horizontalMovement < 0) {
                dx = -speed;
            } else if (horizontalMovement > 0) {
                dx = speed;
            }

            if (verticalMovement < 0) {
                dy = -speed;
            } else if (verticalMovement > 0) {
                dy = speed;
            }

            worldX += dx;
            worldY += dy;

            int currentCol = (worldX + solidArea.x) / gp.tileSize;
            int currentRow = (worldY + solidArea.y) / gp.tileSize;

            if (currentCol == goalCol && currentRow == goalRow) {
                onPath = false;
            }
        }
    }






}

