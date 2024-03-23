package Main;

import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

public class KeyHandler implements KeyListener {
    GamePanel gp;
    public boolean upPressed, downPressed, leftPressed, rightPressed, dashPressed, shotKeyPressed;
    public boolean optionsPressed=false;
    public boolean enterPressed=false;
    public KeyHandler(GamePanel gp){
        this.gp=gp;
    }

    @Override
    public void keyTyped(KeyEvent e) {
    }

    @Override
    public void keyPressed(KeyEvent e) {

        int code = e.getKeyCode();
        //TITLE STATE
        if(gp.gameState==gp.titleState){

            if(code == KeyEvent.VK_W){

                gp.ui.commandNum--;
                if(gp.ui.commandNum<0){
                    gp.ui.commandNum=2;
                }
            }

            if(code == KeyEvent.VK_S){

               gp.ui.commandNum++;
                if(gp.ui.commandNum>2){
                    gp.ui.commandNum=0;
                }
            }

            if(code==KeyEvent.VK_ENTER){
                if(gp.ui.commandNum==0){
                    gp.currentMap=0;
                    gp.gameState= gp.restartLevelState;
                    gp.player.createDatabase();
                }

                if(gp.ui.commandNum==1){
                    gp.player.loadFromDataBase();
                    gp.gameState = gp.playState;
                }
                if(gp.ui.commandNum==2){
                    gp.player.saveIntoDatabase();

                    System.exit(0);
                }
            }
        }
        else if(gp.gameState == gp.playState) {
            if(code == KeyEvent.VK_W){

                upPressed = true;
            }

            if(code == KeyEvent.VK_F){

                shotKeyPressed = true;
            }

            if(code == KeyEvent.VK_S){

                downPressed = true;
            }

            if(code == KeyEvent.VK_A){

                leftPressed = true;
            }

            if(code == KeyEvent.VK_D){

                rightPressed = true;
            }

            if(code == KeyEvent.VK_P){

                if(gp.gameState==gp.playState){
                    gp.gameState=gp.pauseState;
                }
            }

            if(code == KeyEvent.VK_SPACE){

                dashPressed = true;
            }
            if(code == KeyEvent.VK_ESCAPE){
                optionsPressed=true;
                gp.gameState=gp.optionsState;

            }
            if(code == KeyEvent.VK_L) {
                gp.player.saveIntoDatabase();
            }

        }

        else if(gp.gameState == gp.dialogueState ) {
            if(code == KeyEvent.VK_ENTER) {
                gp.gameState = gp.playState;
            }

            if(code==KeyEvent.VK_R){
                gp.gameState=gp.restartLevelState;
                gp.ui.playTime=30;
            }
            if(code == KeyEvent.VK_M) {
                gp.ui.messageOn=false;
                gp.gameState = gp.titleState;
            }

        } else if(gp.gameState == gp.pauseState) {
            if(code == KeyEvent.VK_P) {
                gp.gameState=gp.playState;
            }

        }else if(gp.gameState== gp.optionsState){
           optionsState(code);

        } else if (gp.gameState==gp.gameOverState) {
                if (code == KeyEvent.VK_M) {
                    gp.gameState=gp.titleState;
                }
            if (code == KeyEvent.VK_R) {
                gp.gameState=gp.restartLevelState;
            }
        } else if (gp.gameState== gp.gameOverState1) {
            if (code == KeyEvent.VK_M) {
                gp.gameState=gp.titleState;
            }
            if (code == KeyEvent.VK_R) {
                gp.gameState=gp.restartLevelState;
            }
        }

    }

    public void optionsState(int code){
        if(code==KeyEvent.VK_ESCAPE){
            gp.gameState=gp.playState;
        }
        if(code==KeyEvent.VK_ENTER) {
           enterPressed=true;
        }
        int maxCommandNum=0;
        switch (gp.ui.subState){
            case 0:maxCommandNum=3;
        }
        if(code==KeyEvent.VK_W){
            gp.ui.commandNum--;
            if(gp.ui.commandNum<0){
                gp.ui.commandNum=maxCommandNum;
            }
        }
        if(code==KeyEvent.VK_S){
            gp.ui.commandNum++;
            if(gp.ui.commandNum>maxCommandNum){
                gp.ui.commandNum=0;
            }
        }

    }

    @Override
    public void keyReleased(KeyEvent e) {

        int code = e.getKeyCode();

        if(code == KeyEvent.VK_W){

            upPressed = false;
        }

        if(code == KeyEvent.VK_S){

            downPressed = false;
        }

        if(code == KeyEvent.VK_A){

            leftPressed = false;
        }

        if(code == KeyEvent.VK_D){

            rightPressed = false;
        }

        if(code == KeyEvent.VK_SPACE){

            dashPressed = false;
        }
       // if(code == KeyEvent.VK_ESCAPE){
         //   enterPressed=false;
        //}
        if(code == KeyEvent.VK_F){

            shotKeyPressed = false;
        }
    }
}
