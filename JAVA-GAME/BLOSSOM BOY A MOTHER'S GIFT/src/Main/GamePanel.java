package Main;

import AI.Pathfinder;
import Entity.Entity;
import Entity.Player;
import Exceptions.Invalid_IMAGE_Exception;
import Objects.SuperObject;
import Tile.TileManager;

import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;

public class GamePanel extends JPanel implements Runnable{

    // SETARI ECRAN

    public final int originalTileSize = 16;
    final int scale = 3;

    GamePanel gp;
    public final int tileSize = originalTileSize * scale;
    public final int maxScreenCol = 20;
    public final int maxScreenLine = 12;
    public final int screenWidth = tileSize * maxScreenCol;
    public final int screenHeight = tileSize * maxScreenLine;

    // Setari harta

    public final int maxWorldCol = 26;
    public final int maxWorldRow = 21;
    public final int worldWidth = tileSize * maxWorldCol;
    public final int worldHeight = tileSize * maxWorldRow;
    public final int maxMap=10;
    public int currentMap=0;
    // FPS
    int FPS = 60;



    public KeyHandler keyH = new KeyHandler(this);
    Thread gameThread;

    // ENTITY si OBJECT
    public Player player = new Player(this, keyH);
    public SuperObject[][] obj=new SuperObject[maxMap][100];
    public Entity npc[][]=new Entity[maxMap][10];
    AssetSetter aSetter = new AssetSetter(this);
    public TileManager tileM = new TileManager(this);
    public CollisionChecker cChecker=new CollisionChecker(this);
    public UI ui=new UI(this);
    public Pathfinder pFinder = new Pathfinder(this);
    public ArrayList<Entity> projectileList=new ArrayList<>();




    //GAME STATE
    public int gameState;
    public final int titleState=0;
    public final int playState=1;
    public final int pauseState=2;
    public final int dialogueState=3;
    public final int restartLevelState = 4;
    public final int optionsState=5;
    public final int gameOverState=6;
    public final int gameOverState1=7;

    public GamePanel() throws Invalid_IMAGE_Exception {

        this.setPreferredSize(new Dimension(screenWidth, screenHeight));
        this.setBackground(Color.black);
        this.setDoubleBuffered(true);
        this.addKeyListener(keyH);
        this.setFocusable(true);
    }

    public void SetUpGame(){
        aSetter.setObject();
        aSetter.setNPC();
        gameState=titleState;

    }


    public void restartGame() {

        // RESETAM JUCATORUL, OBIECTELE SI INAMICII
        aSetter.setObject();
        aSetter.setNPC();
        player.setDefaultValues();

        // RESETAM GAMESTATE-UL
        gameState = playState;

        // RESETAM NUMARUL DE FLORI
        player.hasFlower = 5;

        // RESETAM STARILE JUCATORULUI( DASH)
        player.dashCooldown = 0;
        player.score=player.score-(5-player.hasFlower)*5;

    }

    public void startGameThread() {

        gameThread = new Thread(this);
        gameThread.start();
    }

    @Override
    public void run() {

        double drawInterval = (double)1000000000/FPS;
        double nextDrawTime = System.nanoTime() + drawInterval;

        while (gameThread != null) {
            update();
            repaint();
            // masuram cat timp a ramas dupa apelare update() si repaint(), pentru a-l pune pe sleep
            try {
                // transformam timpul ramas din nanosecunde in milisecunde
                double remainingTime = nextDrawTime - System.nanoTime();
                remainingTime = remainingTime/1000000;// urmatorul moment in care se va "desena", adica dupa 0.01666...666 secunde

                if(remainingTime < 0){
                    remainingTime = 0;
                }

                Thread.sleep((long) remainingTime);

                nextDrawTime += drawInterval;
            } catch (InterruptedException e) {
                throw new RuntimeException(e); //e.printStackTrace();
            }
        }
    }

    public void update() {

        if(gameState==playState){
            player.update();
            for(int i=0;i<npc[1].length;i++){
                if(npc[currentMap][i]!=null){
                    npc[currentMap][i].update();
                }
            }
            for(int i=0;i<projectileList.size();i++){
                if(projectileList.get(i) !=null){
                    if(projectileList.get(i).alive==true){
                        projectileList.get(i).update();
                    }
                    if(projectileList.get(i).alive==false){
                        projectileList.remove(i);
                    }
                }

                }
        }
        if(gameState==pauseState){

        }

    }

    public void paintComponent(Graphics g){

        super.paintComponent(g);

        Graphics2D g2 = (Graphics2D) g;

        //Title screen



        if(gameState==titleState){
            ui.draw(g2);
        }
        else {

            tileM.draw(g2);

            for(int i=0;i< obj[1].length;i++)
            {
                if(obj[currentMap][i]!=null){
                    if(obj[currentMap][i].name=="tree"){
                        obj[currentMap][i].drawTree(g2,this);

                    }else {

                        obj[currentMap][i].draw(g2, this);
                    }
                }
            }
            //npc
            for(int i=0;i<npc[1].length;i++){
                if(npc[currentMap][i]!=null){
                    npc[currentMap][i].draw(g2);
                }
            }

            player.draw(g2);
            //UI
            ui.draw(g2);

            for(int i=0;i<projectileList.size();i++){
                if(projectileList.get(i)!=null){
                    projectileList.get(i).draw(g2);
                }
            }
        }


        g2.dispose();
    }
}
