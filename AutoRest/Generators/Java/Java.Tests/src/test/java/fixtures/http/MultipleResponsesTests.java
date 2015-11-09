package fixtures.http;

import com.microsoft.rest.ServiceCallback;
import com.microsoft.rest.ServiceException;
import com.microsoft.rest.ServiceResponse;
import com.microsoft.rest.serializer.JacksonUtils;
import fixtures.http.models.A;
import fixtures.http.models.C;
import fixtures.http.models.D;
import fixtures.http.models.Error;
import org.junit.Assert;
import org.junit.BeforeClass;
import org.junit.Test;

import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

import static org.junit.Assert.fail;

public class MultipleResponsesTests {
    static AutoRestHttpInfrastructureTestService client;
    private CountDownLatch lock = new CountDownLatch(1);

    @BeforeClass
    public static void setup() {
        client = new AutoRestHttpInfrastructureTestServiceImpl("http://localhost.:3000");
    }

    @Test
    public void get200Model204NoModelDefaultError200Valid() throws Exception {
        A result = client.getMultipleResponses().get200Model204NoModelDefaultError200Valid().getBody();
        Assert.assertEquals(A.class, result.getClass());
        Assert.assertEquals("200", result.getStatusCode());
    }

    @Test
    public void get200Model204NoModelDefaultError204Valid() throws Exception {
        A result = client.getMultipleResponses().get200Model204NoModelDefaultError204Valid().getBody();
        Assert.assertNull(result);
    }

    @Test
    public void get200Model204NoModelDefaultError201Invalid() throws Exception {
        try {
            client.getMultipleResponses().get200Model204NoModelDefaultError201Invalid();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(201, ex.getResponse().code());
        }
    }

    @Test
    public void get200Model204NoModelDefaultError202None() throws Exception {
        try {
            A result = client.getMultipleResponses().get200Model204NoModelDefaultError202None().getBody();
        } catch (ServiceException ex) {
            Assert.assertEquals(202, ex.getResponse().code());
        }
    }

    @Test
    public void get200Model204NoModelDefaultError400Valid() throws Exception {
        try {
            client.getMultipleResponses().get200Model204NoModelDefaultError400Valid();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
        }
    }

    @Test
    public void get200Model201ModelDefaultError200Valid() throws Exception {
        A result = client.getMultipleResponses().get200Model201ModelDefaultError200Valid().getBody();
        Assert.assertEquals("200", result.getStatusCode());
    }

    @Test
    public void get200Model201ModelDefaultError201Valid() throws Exception {
        A result = client.getMultipleResponses().get200Model201ModelDefaultError201Valid().getBody();
        Assert.assertEquals("201", result.getStatusCode());
    }

    @Test
    public void get200Model201ModelDefaultError400Valid() throws Exception {
        try {
            client.getMultipleResponses().get200Model201ModelDefaultError400Valid();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
            Error model = new JacksonUtils().getObjectMapper().convertValue(
                    ex.getErrorModel(), Error.class);
            Assert.assertEquals(400, model.getStatus().intValue());
            Assert.assertEquals("client error", model.getMessage());
        }
    }

    @Test
    public void get200ModelA201ModelC404ModelDDefaultError200Valid() throws Exception {
        Object result = client.getMultipleResponses().get200ModelA201ModelC404ModelDDefaultError200Valid().getBody();
        A actual = (A)result;
        Assert.assertEquals("200", actual.getStatusCode());
    }

    @Test
    public void get200ModelA201ModelC404ModelDDefaultError201Valid() throws Exception {
        Object result = client.getMultipleResponses().get200ModelA201ModelC404ModelDDefaultError201Valid().getBody();
        C actual = (C)result;
        Assert.assertEquals("201", actual.getHttpCode());
    }

    @Test
    public void get200ModelA201ModelC404ModelDDefaultError404Valid() throws Exception {
        Object result = client.getMultipleResponses().get200ModelA201ModelC404ModelDDefaultError404Valid().getBody();
        D actual = (D)result;
        Assert.assertEquals("404", actual.getHttpStatusCode());
    }

    @Test
    public void get200ModelA201ModelC404ModelDDefaultError400Valid() throws Exception {
        try {
            client.getMultipleResponses().get200ModelA201ModelC404ModelDDefaultError400Valid();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
            Error model = new JacksonUtils().getObjectMapper().convertValue(
                    ex.getErrorModel(), Error.class);
            Assert.assertEquals(400, model.getStatus().intValue());
            Assert.assertEquals("client error", model.getMessage());
        }
    }

    @Test
    public void get202None204NoneDefaultError202None() throws Exception {
        client.getMultipleResponses().get202None204NoneDefaultError202NoneAsync(new ServiceCallback<Void>() {
            @Override
            public void failure(Throwable t) {
                fail();
            }

            @Override
            public void success(ServiceResponse<Void> response) {
                Assert.assertEquals(202, response.getResponse().code());
                lock.countDown();
            }
        });
        Assert.assertTrue(lock.await(1000, TimeUnit.MILLISECONDS));
    }

    @Test
    public void get202None204NoneDefaultError204None() throws Exception {
        client.getMultipleResponses().get202None204NoneDefaultError204NoneAsync(new ServiceCallback<Void>() {
            @Override
            public void failure(Throwable t) {
                fail();
            }

            @Override
            public void success(ServiceResponse<Void> response) {
                Assert.assertEquals(204, response.getResponse().code());
                lock.countDown();
            }
        });
        Assert.assertTrue(lock.await(1000, TimeUnit.MILLISECONDS));
    }

    @Test
    public void get202None204NoneDefaultError400Valid() throws Exception {
        try {
            client.getMultipleResponses().get202None204NoneDefaultError400Valid();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
            Error model = new JacksonUtils().getObjectMapper().convertValue(
                    ex.getErrorModel(), Error.class);
            Assert.assertEquals(400, model.getStatus().intValue());
            Assert.assertEquals("client error", model.getMessage());
        }
    }

    @Test
    public void get202None204NoneDefaultNone202Invalid() throws Exception {
        client.getMultipleResponses().get202None204NoneDefaultNone202Invalid();
    }

    @Test
    public void get202None204NoneDefaultNone204None() throws Exception {
        client.getMultipleResponses().get202None204NoneDefaultNone204None();
    }

    @Test
    public void get202None204NoneDefaultNone400None() throws Exception {
        try {
            client.getMultipleResponses().get202None204NoneDefaultNone400None();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
        }
    }

    @Test
    public void get202None204NoneDefaultNone400Invalid() throws Exception {
        try {
            client.getMultipleResponses().get202None204NoneDefaultNone400Invalid();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
        }
    }

    @Test
    public void getDefaultModelA200Valid() throws Exception {
        A result = client.getMultipleResponses().getDefaultModelA200Valid().getBody();
        Assert.assertEquals("200", result.getStatusCode());
    }

    @Test
    public void getDefaultModelA200None() throws Exception {
        A result = client.getMultipleResponses().getDefaultModelA200None().getBody();
        Assert.assertNull(result);
    }

    @Test
    public void getDefaultModelA400Valid() throws Exception {
        try {
            client.getMultipleResponses().getDefaultModelA400Valid();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
            A model = new JacksonUtils().getObjectMapper().convertValue(
                    ex.getErrorModel(), A.class);
            Assert.assertEquals("400", model.getStatusCode());
        }
    }

    @Test
    public void getDefaultModelA400None() throws Exception {
        try {
            client.getMultipleResponses().getDefaultModelA400None();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
        }
    }

    @Test
    public void getDefaultNone200Invalid() throws Exception {
        client.getMultipleResponses().getDefaultNone200Invalid();
    }

    @Test
    public void getDefaultNone200None() throws Exception {
        client.getMultipleResponses().getDefaultNone200None();
    }

    @Test
    public void getDefaultNone400Invalid() throws Exception {
        try {
            client.getMultipleResponses().getDefaultNone400Invalid();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
        }
    }

    @Test
    public void getDefaultNone400None() throws Exception {
        try {
            client.getMultipleResponses().getDefaultNone400None();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
        }
    }

    @Test
    public void get200ModelA200None() throws Exception {
        A result = client.getMultipleResponses().get200ModelA200None().getBody();
        Assert.assertNull(result);
    }

    @Test
    public void get200ModelA200Valid() throws Exception {
        A result = client.getMultipleResponses().get200ModelA200Valid().getBody();
        Assert.assertEquals("200", result.getStatusCode());
    }

    @Test
    public void get200ModelA200Invalid() throws Exception {
        Assert.assertEquals(null, client.getMultipleResponses().get200ModelA200Invalid().getBody().getStatusCode());
    }

    @Test
    public void get200ModelA400None() throws Exception {
        try {
            client.getMultipleResponses().get200ModelA400None();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
            Assert.assertNull(ex.getErrorModel());
        }
    }

    @Test
    public void get200ModelA400Valid() throws Exception {
        try {
            client.getMultipleResponses().get200ModelA400Valid();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
        }
    }

    @Test
    public void get200ModelA400Invalid() throws Exception {
        try {
            client.getMultipleResponses().get200ModelA400Invalid();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(400, ex.getResponse().code());
        }
    }

    @Test
    public void get200ModelA202Valid() throws Exception {
        try {
            client.getMultipleResponses().get200ModelA202Valid();
            fail();
        } catch (ServiceException ex) {
            Assert.assertEquals(202, ex.getResponse().code());
        }
    }
}

