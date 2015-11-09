package fixtures.bodystring;

import fixtures.bodystring.models.Colors;
import org.junit.Assert;
import org.junit.BeforeClass;
import org.junit.Test;

import java.util.concurrent.CountDownLatch;

public class EnumOperationsTests {
    static AutoRestSwaggerBATService client;
    private CountDownLatch lock = new CountDownLatch(1);

    @BeforeClass
    public static void setup() {
        client = new AutoRestSwaggerBATServiceImpl("http://localhost:3000");
    }

    @Test
    public void getNotExpandable() throws Exception {
        Colors result = client.getEnumOperations().getNotExpandable().getBody();
        Assert.assertEquals(Colors.RED_COLOR, result);
    }

    @Test
    public void putNotExpandable() throws Exception {
        client.getEnumOperations().putNotExpandable(Colors.RED_COLOR);
    }
}
