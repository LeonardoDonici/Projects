package Objects;

import javax.imageio.ImageIO;
import java.io.IOException;

public class Fountain extends SuperObject {
    public Fountain(){
        name="fountain";

        try {
            image = ImageIO.read(getClass().getResourceAsStream("/Objects/fountain.png"));

        }catch (IOException e) {
            e.printStackTrace();
        }

        collision = true;
    }
}
