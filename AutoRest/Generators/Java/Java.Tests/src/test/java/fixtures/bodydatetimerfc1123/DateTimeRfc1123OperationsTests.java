package fixtures.bodydatetimerfc1123;

import com.microsoft.rest.ServiceException;
import com.microsoft.rest.DateTimeRfc1123;
import org.joda.time.DateTime;
import org.joda.time.DateTimeZone;
import org.junit.Assert;
import org.junit.BeforeClass;
import org.junit.Test;

public class DateTimeRfc1123OperationsTests {
    static AutoRestRFC1123DateTimeTestService client;

    @BeforeClass
    public static void setup() {
        client = new AutoRestRFC1123DateTimeTestServiceImpl("http://localhost.:3000");
    }

    @Test
    public void getNull() throws Exception {
        Assert.assertNull(client.getDatetimerfc1123().getNull().getBody());
    }

    @Test
    public void getInvalidDate() throws Exception {
        try {
            client.getDatetimerfc1123().getInvalid();
            Assert.assertTrue(false);
        } catch (Exception exception) {
            // expected
            Assert.assertEquals(ServiceException.class, exception.getClass());
            Assert.assertTrue(exception.getMessage().contains("Invalid format"));
        }
    }

    @Test
    public void getOverflowDate() throws Exception {
        DateTimeRfc1123 result = client.getDatetimerfc1123().getOverflow().getBody();
        DateTime expected = new DateTime(10000, 1, 1, 00, 00, 00, 0, DateTimeZone.UTC);
        expected = expected.toDateTime(DateTimeZone.UTC);
        Assert.assertEquals(expected, result.getDateTime());
    }

    @Test
    public void getUnderflowDate() throws Exception {
        try {
            client.getDatetimerfc1123().getUnderflow();
            Assert.assertTrue(false);
        } catch (Exception exception) {
            // expected
            Assert.assertEquals(ServiceException.class, exception.getClass());
            Assert.assertTrue(exception.getMessage().contains("Cannot parse"));
        }
    }

    @Test
    public void putUtcMaxDateTime() throws Exception {
        DateTimeRfc1123 body = new DateTimeRfc1123(new DateTime(9999, 12, 31, 23, 59, 59, 0, DateTimeZone.UTC));
        client.getDatetimerfc1123().putUtcMaxDateTime(body);
    }

    @Test
    public void getUtcLowercaseMaxDateTime() throws Exception {
        DateTimeRfc1123 result = client.getDatetimerfc1123().getUtcLowercaseMaxDateTime().getBody();
        DateTime expected = new DateTime(9999, 12, 31, 23, 59, 59, 0, DateTimeZone.UTC);
        Assert.assertEquals(expected, result.getDateTime());
    }

    @Test
    public void getUtcUppercaseMaxDateTime() throws Exception {
        DateTimeRfc1123 result = client.getDatetimerfc1123().getUtcUppercaseMaxDateTime().getBody();
        DateTime expected = new DateTime(9999, 12, 31, 23, 59, 59, 0, DateTimeZone.UTC);
        Assert.assertEquals(expected, result.getDateTime());
    }

    @Test
    public void putUtcMinDateTime() throws Exception {
        DateTimeRfc1123 body = new DateTimeRfc1123(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeZone.UTC));
        client.getDatetimerfc1123().putUtcMinDateTime(body);
    }

    @Test
    public void getUtcMinDateTime() throws Exception {
        DateTimeRfc1123 result = client.getDatetimerfc1123().getUtcMinDateTime().getBody();
        DateTime expected = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeZone.UTC);
        Assert.assertEquals(expected, result.getDateTime());
    }
}
