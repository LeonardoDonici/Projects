package Entity;

import Main.GamePanel;
import Main.KeyHandler;
import Objects.Bullet;
import Objects.Flower;


import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.sql.*;
import java.util.Random;


public class Player extends Entity {

    GamePanel gp;
    KeyHandler keyH;
    Graphics2D g2;
    public final int screenX;
    public final int screenY;
    public int hasFlower=5;

    private Connection connection;
    String dbFilePath = "db/tiobe.db";
    String absolutePath = new File(dbFilePath).getAbsolutePath();

    public int score=0;
    Font arial_20;
    public Player(GamePanel gp, KeyHandler keyH) {
        super(gp);
        arial_20 = new Font("Arial2", Font.BOLD, 20);
        this.gp = gp;
        this.keyH = keyH;

        screenX = gp.screenWidth/2 - (gp.tileSize/2);
        screenY = gp.screenHeight/2 - (gp.tileSize/2);

        solidArea=new Rectangle();
        solidArea.x=8;
        solidArea.y=16;

        solidAreaDefaultX=solidArea.x;
        solidAreaDefaultY=solidArea.y;
        solidArea.width=22;
        solidArea.height=22;
        projectile=new Bullet(gp);

        try {
            connection = DriverManager.getConnection("jdbc:sqlite:" + absolutePath);

        } catch (SQLException e) {
            e.printStackTrace();
        }

        setDefaultValues();
        getPlayerImage();

    }

    public void setDefaultValues() {

        worldX = 1100;
        worldY = 790;
        speed = 8;
        direction = "down";
    }

    public void update() {

        if (isDashing) {
            dashCounter++;
            if (dashCounter >= dashDuration) {
                isDashing = false;
                dashCounter = 0;
            }
            switch (direction) {
                case "up":
                    worldY -= speed * 8;
                    break;
                case "down":
                    worldY += speed * 8;
                    break;
                case "left":
                    worldX -= speed * 8;
                    break;
                case "right":
                    worldX += speed * 8;
                    break;
            }
        }



        if(keyH.upPressed || keyH.downPressed || keyH.leftPressed || keyH.rightPressed) {

            if (keyH.upPressed) {
                direction = "up";

            } else if (keyH.downPressed) {
                direction = "down";

            } else if (keyH.leftPressed) {
                direction = "left";

            } else if (keyH.rightPressed) {
                direction = "right";

            }
            // VERIFICARE COLIZIUNE CU PERETI SI OBIECTE
            collisionOn=false;
            gp.cChecker.checkTile(this);

            int objIndex = gp.cChecker.checkObject(this, true);
            pickUPObject(objIndex);
            interactFountain(objIndex);

           int npcindex=gp.cChecker.checkEntity(this, gp.npc);
            interactNPC(npcindex);



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

        if(gp.keyH.shotKeyPressed==true && projectile.alive==false){
            projectile.set(worldX,worldY,direction,true,this);

            gp.projectileList.add(projectile);
        }

        // DASH SI COLIZIUNI CU DASH
        if (!isDashing && keyH.dashPressed && dashCooldown <= 0) {
            score-=2;
            isDashing = true;
            dashCounter = 0;
            dashCooldown = 120;
        }
        if (dashCooldown > 0) {
            dashCooldown--;
        }


    }

    public void pickUPObject(int i){
        if(i!=999){

           String objectname=gp.obj[gp.currentMap][i].name;

           switch(objectname) {
               case "flower":
                   score += 5;
                   hasFlower--;
                   gp.obj[gp.currentMap][i] = null;
                   if (hasFlower == 0) {
                       if (gp.currentMap == 0){
                           teleport(1, 19, 18);
                            gp.gameState=gp.dialogueState;
                            gp.ui.showMessage("\n\nLevel up! Get ready for new challenges in Level 2. Go find the magic fountain for useful tips.\n Don't forget to use your dash but remember that your score will decrease every time you will use IT \n Good luck! \n \n  PRES R FOR RESTART         PRESS ENTER TO CONTINUE      PRESS P FOR PAUSE ");
                       }

                       else if (gp.currentMap==1) {
                           teleport(2,20,4);

                           gp.gameState=gp.dialogueState;
                           gp.ui.showMessage("\n Welcome to Level 3: Unleashed Chaos! Prepare yourself for the most exhilarating test yet as you navigate \nthrough this treacherous realm. Stay one step ahead of the relentless tracking bullets unleashed \n by the Goblins . Seek solace in the mystical waters of the Magic Fountain, where invaluable insights and \n strategies await. Harness your skills, dodge the bullets, unlock the fountain's \n secrets, and emerge triumphant from this epic challenge\n PRES R FOR RESTART         PRESS ENTER TO CONTINUE      PRESS P FOR PAUSE");



                       }else if(gp.currentMap==2){
                           gp.gameState=gp.gameOverState;

                       }
                   }
                   int ok;
                   do {
                       Random rand = new Random();
                       int randomX = rand.nextInt((1248 - 48) / 48) * 48;
                       int randomY = rand.nextInt((1008 - 48) / 48) * 48;
                       if (randomX <= 48) randomX = randomX + (48 - randomX);
                       if (randomY <= 48) randomY = randomY + (48 - randomY);
                       ok = 1;
                       for (i = 2; i < 50; i++) {
                           if (gp.obj[gp.currentMap][i].worldX == randomX && gp.obj[gp.currentMap][i].worldY == randomY) {
                               ok = 0;
                           }
                       }
                       if (ok == 1) {
                           Flower flower = new Flower(randomX, randomY, gp.tileSize);
                           gp.obj[gp.currentMap][0] = flower;
                       }


                   } while (ok == 0);

               case "tree":
                   break;

               case "fountain":
                   break;

           }
        }
    }

        public void interactNPC(int i){
            if(i!=999) {
                if (gp.currentMap == 0) {
                    gp.gameState = gp.dialogueState;
                    gp.ui.showMessage("\n Alas! The Goblins have finally caught up with you, ensnaring you in their clutches.Despite your valiant\n efforts, they proved too formidable this time. Do not despair, brave adventurer, for setbacks are mere \nstepping stones on the path to victory. Gather your strength, learn from this encounter and rise again with \nunwavering determination.Your journey is not over yet.Dust yourself off, and go ahead towards triumph!\n\n          PRESS R TO RESTART                   PRESS M FOR MAIN MENU");
                }

            }
        }


        public void interactFountain(int i){
                if(i==1 && gp.currentMap==0)
                {
                    gp.gameState=gp.dialogueState;
                    gp.ui.showMessage("\nCaution: Beware of the Goblin's keen eyesight! If it spots you , it will relentlessly give chase.\n However, fear not! You possess a remarkable power:a swift dash to outmaneuver the Goblin. \n Your objective in this level is to collect 5 flowers scattered across the terrain. Keep your eyes peeled  for\nthese vibrant blooms while avoiding the Goblin's gaze . Use your dash wisely to elude its pursuit and reach \n the flowers safely . May your agility and determination guide you to victory! Good luck!\n    PRESS R FOR RESTART       PRESS SPACE FOR DASH            PRESS ENTER TO CONTINUE");
                }else if(i==1 && gp.currentMap==1){
                    gp.gameState=gp.dialogueState;
                    gp.ui.showMessage("\n Level 2: Danger Lurks! Collect 5 flowers, but beware the enhanced Goblins! Not only will\n they relentlessly pursue you, but they're also armed with deadly projectiles.\n Stay one step ahead, utilize your dash wisely, and keep evading their shots. \n Your mission is clear: gather the flowers while surviving the onslaught. Good luck, brave adventurer!\n\nPRESS R TO RESTART     PRESS ENTER TO CLOSE THIS WINDOW   PRESS P FOR PAUSE");
                } else if (i==1 && gp.currentMap==2) {
                    gp.gameState=gp.dialogueState;
                    gp.ui.showMessage("\n Level 3: Perilous Pursuit! The stakes have escalated! Prepare for a relentless chase as the \nGoblins now possess an uncanny ability—bullets that track your every move. Evade both the persistent \nGoblins and their homing projectiles as you navigate the treacherous terrain. \nKeep your reflexes sharp, master your dash, and gather the 5 flowers amidst this heightened\n danger. Remember, victory lies in your resilience and cunning. Good luck, valiant hero! \n PRESS R TO RESTART     PRESS ENTER TO CLOSE THIS WINDOW   PRESS P FOR PAUSE");
                }
        }

    public void draw(Graphics2D g2) {

        BufferedImage image = null;

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

    public void getPlayerImage() {

        try {
            BufferedImage spriteSheet = ImageIO.read(getClass().getResourceAsStream("/player/player.png"));

            int spriteWidth = gp.originalTileSize; 
            int spriteHeight = gp.originalTileSize;

            up1 = spriteSheet.getSubimage(0, 0, spriteWidth, spriteHeight);
            up2 = spriteSheet.getSubimage(spriteWidth, 0, spriteWidth, spriteHeight);
            left1 = spriteSheet.getSubimage(2 * spriteWidth, 0, spriteWidth, spriteHeight);
            left2 = spriteSheet.getSubimage(3 * spriteWidth, 0, spriteWidth, spriteHeight);
            right1 = spriteSheet.getSubimage(4 * spriteWidth, 0, spriteWidth, spriteHeight);
            right2 = spriteSheet.getSubimage(5 * spriteWidth, 0, spriteWidth, spriteHeight);
            down1 = spriteSheet.getSubimage(6 * spriteWidth, 0, spriteWidth, spriteHeight);
            down2 = spriteSheet.getSubimage(7 * spriteWidth, 0, spriteWidth, spriteHeight);

        } catch(IOException e) {
            e.printStackTrace();
        }
    }

    public void teleport(int map, int col, int row) {
        gp.currentMap = map;
        gp.player.worldX = gp.tileSize * col;
        gp.player.worldY = gp.tileSize * row;
        gp.player.direction = "down";
        hasFlower=5;
        gp.ui.playTime=30;
    }

    public void createDatabase() {
        try {
            Statement statement = connection.createStatement();

//            String query = "DROP TABLE IF EXISTS playerTable";
//            statement.executeUpdate(query);
//            System.out.println("Table deleted successfully.");

            String query = "CREATE TABLE IF NOT EXISTS playerTable ("
                    + "id INTEGER PRIMARY KEY AUTOINCREMENT,"
                    + "currentMap INTEGER,"
                    + "worldX INTEGER,"
                    + "worldY INTEGER,"
                    + "Score INTEGER,"
                    + "Flowers INTEGER,"
                    + "TimeLeft INTEGER"
                    + ")";
            statement.executeUpdate(query);
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void saveIntoDatabase() {
        try {
            String insertQuery = "INSERT INTO playerTable (currentMap, worldX, worldY,Score,Flowers,TimeLeft) VALUES (?, ?, ?,?,?,?)";
            PreparedStatement insertStatement = connection.prepareStatement(insertQuery);

            insertStatement.setInt(1, gp.currentMap);
            insertStatement.setInt(2, this.worldX);
            insertStatement.setInt(3, this.worldY);
            insertStatement.setInt(4, score);
            insertStatement.setInt(5, hasFlower);
            insertStatement.setDouble(6, gp.ui.playTime);

            System.out.println("Saved");
            insertStatement.executeUpdate();

            // Close the statement
            insertStatement.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    public void loadFromDataBase() {
        try (Statement statement = connection.createStatement()) {
            ResultSet resultSet = statement.executeQuery("SELECT * FROM playerTable ORDER BY id DESC LIMIT 1");

            // Assuming you have only one row in the scores table
            if (resultSet.next()) {
                gp.currentMap = resultSet.getInt("currentMap");
                this.worldX = resultSet.getInt("worldX");
                this.worldY = resultSet.getInt("worldY");
                score=resultSet.getInt("Score");
                hasFlower=resultSet.getInt("Flowers");
                gp.ui.playTime=resultSet.getDouble("TimeLeft");
            }
            resultSet.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

}
