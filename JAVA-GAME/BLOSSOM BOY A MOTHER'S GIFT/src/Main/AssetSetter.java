package Main;

import Enemy.Goblin;
import Enemy.WalkingGoblin;
import Objects.*;

public class AssetSetter {


    GamePanel gp;

    public AssetSetter(GamePanel gp) {

        this.gp = gp;

    }

    public void setObject() {

        int mapNum=0;
        gp.obj[mapNum][0] = new Flower(500, 100, gp.tileSize);

        gp.obj[mapNum][1]=new Fountain();
        gp.obj[mapNum][1].worldX=1000;
        gp.obj[mapNum][1].worldY=800;

        for (int i = 2; i < 50; i++) {

                gp.obj[mapNum][i] = new Tree(gp.worldWidth, gp.worldHeight, gp.tileSize);

        }
        mapNum=1;


        gp.obj[mapNum][0] = new Flower(500, 100, gp.tileSize);

        gp.obj[mapNum][1]=new Fountain();
        gp.obj[mapNum][1].worldX=1000;
        gp.obj[mapNum][1].worldY=800;

        for (int i = 2; i < 50; i++) {
            gp.obj[mapNum][i] = new Tree1(gp.worldWidth, gp.worldHeight, gp.tileSize);

        }
        mapNum=2;

        gp.obj[mapNum][0] = new Flower(500, 100, gp.tileSize);

        gp.obj[mapNum][1]=new Fountain();
        gp.obj[mapNum][1].worldX=1000;
        gp.obj[mapNum][1].worldY=800;

        for (int i = 2; i < 50; i++) {
            gp.obj[mapNum][i] = new Tree(gp.worldWidth, gp.worldHeight, gp.tileSize);

        }

    }
    public void setNPC(){
        int mapNum=0;

        gp.npc[mapNum][0]=new Goblin(gp);
        gp.npc[mapNum][0].worldX=gp.tileSize*3;
        gp.npc[mapNum][0].worldY=gp.tileSize*18;
        gp.npc[mapNum][1]=new Goblin(gp);
        gp.npc[mapNum][1].worldX=gp.tileSize*9;
        gp.npc[mapNum][1].worldY=gp.tileSize*19;
        gp.npc[mapNum][2]=new Goblin(gp);
        gp.npc[mapNum][2].worldX=gp.tileSize*3;
        gp.npc[mapNum][2].worldY=gp.tileSize*3;
        gp.npc[mapNum][3]=new Goblin(gp);
        gp.npc[mapNum][3].worldX=gp.tileSize*15;
        gp.npc[mapNum][3].worldY=gp.tileSize*3;
        gp.npc[mapNum][4]=new Goblin(gp);
        gp.npc[mapNum][4].worldX=gp.tileSize*10;
        gp.npc[mapNum][4].worldY=gp.tileSize*10;

        gp.npc[mapNum][5]=new Goblin(gp);
        gp.npc[mapNum][5].worldX=gp.tileSize*18;
        gp.npc[mapNum][5].worldY=gp.tileSize*15;

        gp.npc[mapNum][6]=new Goblin(gp);
        gp.npc[mapNum][6].worldX=gp.tileSize*16;
        gp.npc[mapNum][6].worldY=gp.tileSize*8;

        mapNum=1;

        gp.npc[mapNum][0]=new Goblin(gp);
        gp.npc[mapNum][0].worldX=gp.tileSize*3;
        gp.npc[mapNum][0].worldY=gp.tileSize*18;
        gp.npc[mapNum][1]=new Goblin(gp);
        gp.npc[mapNum][1].worldX=gp.tileSize*9;
        gp.npc[mapNum][1].worldY=gp.tileSize*19;
        gp.npc[mapNum][2]=new Goblin(gp);
        gp.npc[mapNum][2].worldX=gp.tileSize*3;
        gp.npc[mapNum][2].worldY=gp.tileSize*3;
        gp.npc[mapNum][3]=new Goblin(gp);
        gp.npc[mapNum][3].worldX=gp.tileSize*15;
        gp.npc[mapNum][3].worldY=gp.tileSize*3;
        gp.npc[mapNum][4]=new Goblin(gp);
        gp.npc[mapNum][4].worldX=gp.tileSize*10;
        gp.npc[mapNum][4].worldY=gp.tileSize*10;

        gp.npc[mapNum][5]=new Goblin(gp);
        gp.npc[mapNum][5].worldX=gp.tileSize*18;
        gp.npc[mapNum][5].worldY=gp.tileSize*15;

        gp.npc[mapNum][6]=new Goblin(gp);
        gp.npc[mapNum][6].worldX=gp.tileSize*12;
        gp.npc[mapNum][6].worldY=gp.tileSize*11;


        mapNum=2;
        gp.npc[mapNum][0]=new Goblin(gp);
        gp.npc[mapNum][0].worldX=gp.tileSize*3;
        gp.npc[mapNum][0].worldY=gp.tileSize*18;
        gp.npc[mapNum][1]=new Goblin(gp);
        gp.npc[mapNum][1].worldX=gp.tileSize*9;
        gp.npc[mapNum][1].worldY=gp.tileSize*19;
        gp.npc[mapNum][2]=new Goblin(gp);
        gp.npc[mapNum][2].worldX=gp.tileSize*3;
        gp.npc[mapNum][2].worldY=gp.tileSize*3;
        gp.npc[mapNum][3]=new Goblin(gp);
        gp.npc[mapNum][3].worldX=gp.tileSize*15;
        gp.npc[mapNum][3].worldY=gp.tileSize*3;

        gp.npc[mapNum][4]=new WalkingGoblin(gp);
        gp.npc[mapNum][4].worldX=gp.tileSize*10;
        gp.npc[mapNum][4].worldY=gp.tileSize*10;

        gp.npc[mapNum][5]=new WalkingGoblin(gp);
        gp.npc[mapNum][5].worldX=gp.tileSize*18;
        gp.npc[mapNum][5].worldY=gp.tileSize*15;

        gp.npc[mapNum][6]=new WalkingGoblin(gp);
        gp.npc[mapNum][6].worldX=gp.tileSize*12;
        gp.npc[mapNum][6].worldY=gp.tileSize*19;

        gp.npc[mapNum][7]=new Goblin(gp);
        gp.npc[mapNum][7].worldX=gp.tileSize*18;
        gp.npc[mapNum][7].worldY=gp.tileSize*7;
    }
}

