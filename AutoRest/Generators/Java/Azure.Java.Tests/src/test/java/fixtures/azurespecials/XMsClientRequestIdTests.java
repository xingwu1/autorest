package fixtures.azurespecials;

import com.microsoft.rest.CustomHeaderInterceptor;
import com.microsoft.rest.ServiceException;
import com.microsoft.rest.ServiceResponse;
import com.microsoft.rest.credentials.TokenCredentials;
import com.squareup.okhttp.Interceptor;
import org.junit.Assert;
import org.junit.BeforeClass;
import org.junit.Test;

import java.util.Map;
import java.util.UUID;

import static org.junit.Assert.fail;

public class XMsClientRequestIdTests {
    static AutoRestAzureSpecialParametersTestClient client;

    @BeforeClass
    public static void setup() {
        client = new AutoRestAzureSpecialParametersTestClientImpl("http://localhost.:3000", new TokenCredentials(null, UUID.randomUUID().toString()));
        client.setSubscriptionId("1234-5678-9012-3456");
    }

    @Test
    public void get() throws Exception {
        for (Interceptor i : client.getClientInterceptors()) {
            if (i instanceof CustomHeaderInterceptor) {
                ((CustomHeaderInterceptor)i).removeHeader("x-ms-client-request-id");
            }
        }
        CustomHeaderInterceptor interceptor = new CustomHeaderInterceptor("x-ms-client-request-id", "9C4D50EE-2D56-4CD3-8152-34347DC9F2B0");
        client.getClientInterceptors().add(interceptor);
        ServiceResponse<Void> response = client.getXMsClientRequestId().get();
        client.getClientInterceptors().remove(interceptor);
        Assert.assertEquals(200, response.getResponse().code());
    }

    @Test
    public void paramGet() throws Exception {
        for (Interceptor i : client.getClientInterceptors()) {
            if (i instanceof CustomHeaderInterceptor) {
                ((CustomHeaderInterceptor)i).removeHeader("x-ms-client-request-id");
            }
        }
        ServiceResponse<Void> response = client.getXMsClientRequestId().paramGet("9C4D50EE-2D56-4CD3-8152-34347DC9F2B0");
        Assert.assertEquals(200, response.getResponse().code());
    }
}
