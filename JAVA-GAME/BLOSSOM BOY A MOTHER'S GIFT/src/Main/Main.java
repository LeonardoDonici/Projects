package Main;
import Exceptions.Invalid_IMAGE_Exception;
import javax.swing.*;

public class Main {

    public static void main(String[] args) throws Invalid_IMAGE_Exception{
        try {
                JFrame window = new JFrame();

                window.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
                window.setResizable(false);
                window.setTitle("Blossom Boy: A Mother's Gift");

                GamePanel gamePanel = new GamePanel();
                window.add(gamePanel);

                window.pack();

                window.setLocationRelativeTo(null);
                window.setVisible(true);

                gamePanel.SetUpGame();
                gamePanel.startGameThread();
        }catch (Exception e){
                e.printStackTrace();
                throw new Invalid_IMAGE_Exception();
        }


    }
}
