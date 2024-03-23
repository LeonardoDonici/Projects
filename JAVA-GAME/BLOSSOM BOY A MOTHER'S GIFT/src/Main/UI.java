package Main;

import Objects.Flower;
import Main.GamePanel;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.text.DecimalFormat;

public class UI {
    GamePanel gp;
    Font arial_40;
    Font arial_80B;
    Font arial_20;
    BufferedImage flowerImage;
    public double playTime= 30 ;
    DecimalFormat dFormat=new DecimalFormat("#0.00");
    public boolean messageOn = false;
    Graphics2D g2;
    public String message = "";
    public int commandNum = 0;
    int subState = 0;

    public UI(GamePanel gp) {

        this.gp = gp;
        arial_40 = new Font("Arial", Font.BOLD, 40);
        arial_80B = new Font("Arial1", Font.BOLD, 80);
        arial_20 = new Font("Arial2", Font.BOLD, 20);

    }

    public void draw(Graphics2D g2) {
        this.g2 = g2;

        //Title State
        if (gp.gameState == gp.titleState) {
            drawTitleScreen();
        }

        if (gp.gameState == gp.dialogueState) {
            drawDialogueScreen();
        }

        if (gp.gameState == gp.playState) {

            Flower flower = new Flower();
            flowerImage = flower.image;
        }
        if (gp.gameState == gp.pauseState) {
            drawPauseScreen();
        }
        if (gp.gameState == gp.restartLevelState) {
            gp.restartGame();

        }
        if(gp.gameState==gp.gameOverState1){
            drawGameOverScreen1();
        }


        if (gp.gameState == gp.playState) {
           if(gp.currentMap==2) {
               if (playTime > 0.00) {
               g2.setFont(arial_20);
               g2.setColor(Color.white);
               playTime -= (double) 1 / 60;
               g2.drawString("Time: " + dFormat.format(playTime), gp.tileSize * 14, 65);
               }else {
                    gp.gameState=gp.gameOverState1;
               }
           }
            g2.setFont(arial_40);
            g2.setColor(Color.white);
            g2.drawImage(flowerImage, gp.tileSize / 2, gp.tileSize / 2, gp.tileSize, gp.tileSize, null);
            g2.drawString("x " + gp.player.hasFlower, 74, 65);
            g2.setFont(arial_20);
            g2.drawString("Score : "+ gp.player.score,74,100);
        }

        int x = 3 * gp.tileSize;
        int y = 1 * gp.tileSize;

        if (messageOn == true) {
            g2.setFont(g2.getFont().deriveFont(15F));

            for (String line : message.split("\n")) {
                g2.drawString(line, x, y);
                y += 30;
            }
            if (gp.gameState == gp.playState) {
                messageOn = false;
            }
        }
        if (gp.gameState == gp.optionsState) {
            drawOptionsScreen();
        }
        if (gp.gameState == gp.gameOverState) {
            drawGameOverScreen();
        }

    }

    private void drawGameOverScreen() {

        g2.setColor(new Color(0,0,0,150));
        g2.fillRect(0,0,gp.screenWidth,gp.screenHeight);

        int x,y;
        String text;

        g2.setFont(g2.getFont().deriveFont(Font.BOLD,110F));
        //SHADOW
        text="Game Over";
        g2.setColor(Color.black);
        x=getXforCenteredText(text);
        y=gp.tileSize*4;
        g2.drawString(text,x,y);
        //MAIN
        g2.setColor(Color.white);
        g2.drawString(text,x-4,y-4);

        g2.setFont(g2.getFont().deriveFont(Font.BOLD,50F));
        text="YOU HAVE DONE IT !!";
        g2.drawString(text,x+50,y+100);
        g2.setFont(g2.getFont().deriveFont(Font.BOLD,50F));
        text="YOU WON ";
        g2.drawString(text,x+170,y+200);
        g2.setFont(g2.getFont().deriveFont(Font.BOLD,40F));

        text="YOUR FINAL SCORE: ";
        g2.drawString(text,x+100,y+300);
        g2.setFont(g2.getFont().deriveFont(Font.BOLD,30F));

        text=Integer.toString(gp.player.score);
        g2.drawString(text,x+520,y+300);
        g2.setFont(g2.getFont().deriveFont(Font.BOLD,30F));

        text="PRESS M FOR MAIN MENU OR R TO RESTART";
        g2.drawString(text,x-20,y+350);
        g2.setFont(g2.getFont().deriveFont(Font.BOLD,10F));

    }


    private void drawGameOverScreen1() {

        g2.setColor(new Color(0,0,0,150));
        g2.fillRect(0,0,gp.screenWidth,gp.screenHeight);

        int x,y;
        String text;

        g2.setFont(g2.getFont().deriveFont(Font.BOLD,110F));
        //SHADOW
        text="GAME OVER";
        g2.setColor(Color.black);
        x=getXforCenteredText(text);
        y=gp.tileSize*4;
        g2.drawString(text,x,y);
        //MAIN
        g2.setColor(Color.white);
        g2.drawString(text,x-4,y-4);

        g2.setFont(g2.getFont().deriveFont(Font.BOLD,50F));
        text="TIME'S UP !!";
        g2.drawString(text,x+170,y+100);
        g2.setFont(g2.getFont().deriveFont(Font.BOLD,50F));
        text="YOU LOST";
        g2.drawString(text,x+170,y+200);
        g2.setFont(g2.getFont().deriveFont(Font.BOLD,40F));

        text="YOUR FINAL SCORE: ";
        g2.drawString(text,x+100,y+300);
        g2.setFont(g2.getFont().deriveFont(Font.BOLD,30F));

        text=Integer.toString(gp.player.score);
        g2.drawString(text,x+520,y+300);
        g2.setFont(g2.getFont().deriveFont(Font.BOLD,30F));

        text="PRESS M FOR MAIN MENU OR R TO RESTART";
        g2.drawString(text,x-20,y+350);
        g2.setFont(g2.getFont().deriveFont(Font.BOLD,10F));

    }



    public void drawTitleScreen() {

        g2.setColor(new Color(39, 191, 246));
        g2.fillRect(0, 0, gp.screenWidth, gp.screenHeight);
        //Title Name
        g2.setFont(g2.getFont().deriveFont(Font.BOLD, 32F));
        String text = "Blossom Boy A Mother's Gift";
        int x = getXforCenteredText(text);
        int y = gp.tileSize * 3;

        //SHADOW
        g2.setColor(Color.black);
        g2.drawString(text, x + 5, y + 5);

        g2.setColor(Color.white);
        g2.drawString(text, x, y);

        //BOY IMAGE
        x = gp.screenWidth / 2 - (gp.tileSize * 2) / 2;
        y += gp.tileSize * 2;
        g2.drawImage(gp.player.down1, x, y, gp.tileSize * 2, gp.tileSize * 2, null);

        g2.setFont(g2.getFont().deriveFont(Font.BOLD, 40F));

        text = "NEW GAME";
        x = getXforCenteredText(text);
        y += gp.tileSize * 4;
        g2.drawString(text, x, y);
        if (commandNum == 0) {
            g2.drawString("->", x - gp.tileSize, y);
        }

        text = "LOAD GAME";
        x = getXforCenteredText(text);
        y += gp.tileSize;
        g2.drawString(text, x, y);
        if (commandNum == 1) {
            g2.drawString("->", x - gp.tileSize, y);
        }
        text = "QUIT";
        x = getXforCenteredText(text);
        y += gp.tileSize;
        g2.drawString(text, x, y);
        if (commandNum == 2) {
            g2.drawString("->", x - gp.tileSize, y);
        }
    }

    public void drawPauseScreen() {
        String text = "PAUSED";

        g2.setFont(arial_80B);
        g2.setColor(Color.yellow);
        int x = getXforCenteredText(text);
        int y = gp.screenHeight / 2;
        g2.drawString(text, x, y);
    }

    public int getXforCenteredText(String text) {
        int length = (int) g2.getFontMetrics().getStringBounds(text, g2).getWidth();
        int x = gp.screenWidth / 2 - length / 2;
        return x;
    }

    public void drawDialogueScreen() {
        // WINDOW
        int x = gp.tileSize * 2;
        int y = gp.tileSize / 2;
        int width = gp.screenWidth - (gp.tileSize * 4);
        int height = gp.tileSize * 5;

        drawSubWindow(x, y, width, height);

    }

    public void drawSubWindow(int x, int y, int width, int height) {
        Color c = new Color(0, 0, 0, 150);
        g2.setColor(c);
        g2.fillRoundRect(x, y, width, height, 35, 35);

        c = new Color(255, 255, 255);
        g2.setColor(c);
        g2.setStroke(new BasicStroke(5));
        g2.drawRoundRect(x + 5, y + 5, width - 10, height - 10, 25, 25);


    }

    public void showMessage(String text) {

        message = text;
        messageOn = true;
    }

    public void drawOptionsScreen() {

        g2.setColor(Color.white);
        g2.setFont(g2.getFont().deriveFont(32F));

        int frameX = gp.tileSize * 6;
        int frameY = gp.tileSize;
        int frameWidth = gp.tileSize * 8;
        int frameHeight = gp.tileSize * 10;
        drawSubWindow(frameX, frameY, frameWidth, frameHeight);

        switch (subState) {

            case 0:
                options_top(frameX, frameY);
                break;
            case 1:
                gp.gameState = gp.playState;
                break;
            case 2:
                options_control(frameX, frameY);
                break;
            case 3:
                System.exit(0);
                break;

        }
        gp.keyH.enterPressed=false;
    }

    public void options_top(int frameX, int frameY) {
        int textX;
        int textY;

        String text = "Options";
        textX = getXforCenteredText(text);
        textY = frameY + gp.tileSize;
        g2.drawString(text, textX, textY);

        textX = frameX + gp.tileSize;
        textY += gp.tileSize * 2;
        g2.drawString("Resume Game", textX, textY);
        if (commandNum == 0) {
            g2.drawString(">", textX - 25, textY);
            if (gp.keyH.enterPressed == true) {
                gp.gameState=gp.playState;
            }
        }


        textY += gp.tileSize;
        g2.drawString("Controls", textX, textY);
        if (commandNum == 1) {
            g2.drawString(">", textX - 25, textY);
            if (gp.keyH.enterPressed == true) {
                subState = 2;
                commandNum = 0;
            }
        }

        textY += gp.tileSize;
        g2.drawString("Exit Game", textX, textY);
        if (commandNum == 2) {
            if (gp.keyH.enterPressed == true) {
                subState = 3;
            }
            g2.drawString(">", textX - 25, textY);
        }


        textY += gp.tileSize*2;
        g2.drawString("Main Menu", textX, textY);
        if (commandNum == 3) {
            g2.drawString(">", textX - 25, textY);
            if (gp.keyH.enterPressed == true) {
                gp.gameState=gp.titleState;
            }
        }

    }

    public void options_control(int frameX, int frameY) {
        int textX;
        int textY;
        String text = "Controls";
        textX = getXforCenteredText(text);
        textY = frameY + gp.tileSize;
        g2.drawString(text, textX, textY);

        textX = frameX + gp.tileSize;
        textY += gp.tileSize;
        g2.drawString("Move", textX, textY);
        textY += gp.tileSize;
        g2.drawString("Dash", textX, textY);
        textY += gp.tileSize;
        g2.drawString("Pause", textX, textY);
        textY += gp.tileSize;
        g2.drawString("Restart", textX, textY);
        textY += gp.tileSize;


        textX = frameX + gp.tileSize * 5;
        textY = frameY + gp.tileSize * 2;

        g2.drawString("WASD", textX, textY);
        textY += gp.tileSize;
        g2.drawString("SPACE", textX, textY);
        textY += gp.tileSize;
        g2.drawString("P", textX, textY);
        textY += gp.tileSize;
        g2.drawString("R", textX, textY);
        textY += gp.tileSize;

        //BACK
        textX = frameX + gp.tileSize;
        textY = frameY + gp.tileSize * 9;
        g2.drawString("BACK", textX, textY);

        if (commandNum == 0) {
            g2.drawString(">", textX - 25, textY);
            if (gp.keyH.enterPressed == true) {
                subState = 0;
            }
        }

    }

}

