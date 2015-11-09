/**
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See License.txt in the project root for
 * license information.
 * 
 * Code generated by Microsoft (R) AutoRest Code Generator 0.13.0.0
 * Changes may cause incorrect behavior and will be lost if the code is
 * regenerated.
 */

package fixtures.azurespecials;

import com.microsoft.rest.ServiceCallback;
import com.microsoft.rest.ServiceException;
import com.microsoft.rest.ServiceResponse;
import com.squareup.okhttp.ResponseBody;
import retrofit.Call;
import retrofit.http.Header;
import retrofit.http.Path;
import retrofit.http.POST;
import retrofit.http.Query;

/**
 * An instance of this class provides access to all the operations defined
 * in SubscriptionInCredentials.
 */
public interface SubscriptionInCredentials {
    /**
     * The interface defining all the services for SubscriptionInCredentials to be
     * used by Retrofit to perform actually REST calls.
     */
    interface SubscriptionInCredentialsService {
        @POST("/azurespecials/subscriptionId/method/string/none/path/global/1234-5678-9012-3456/{subscriptionId}")
        Call<ResponseBody> postMethodGlobalValid(@Path("subscriptionId") String subscriptionId, @Header("accept-language") String acceptLanguage);

        @POST("/azurespecials/subscriptionId/method/string/none/path/global/null/{subscriptionId}")
        Call<ResponseBody> postMethodGlobalNull(@Path("subscriptionId") String subscriptionId, @Header("accept-language") String acceptLanguage);

        @POST("/azurespecials/subscriptionId/method/string/none/path/globalNotProvided/1234-5678-9012-3456/{subscriptionId}")
        Call<ResponseBody> postMethodGlobalNotProvidedValid(@Path("subscriptionId") String subscriptionId, @Query("api-version") String apiVersion, @Header("accept-language") String acceptLanguage);

        @POST("/azurespecials/subscriptionId/path/string/none/path/global/1234-5678-9012-3456/{subscriptionId}")
        Call<ResponseBody> postPathGlobalValid(@Path("subscriptionId") String subscriptionId, @Header("accept-language") String acceptLanguage);

        @POST("/azurespecials/subscriptionId/swagger/string/none/path/global/1234-5678-9012-3456/{subscriptionId}")
        Call<ResponseBody> postSwaggerGlobalValid(@Path("subscriptionId") String subscriptionId, @Header("accept-language") String acceptLanguage);

    }
    /**
     * POST method with subscriptionId modeled in credentials.  Set the credential subscriptionId to '1234-5678-9012-3456' to succeed
     *
     * @throws ServiceException exception thrown from REST call
     * @return the ServiceResponse object if successful.
     */
    ServiceResponse<Void> postMethodGlobalValid() throws ServiceException;

    /**
     * POST method with subscriptionId modeled in credentials.  Set the credential subscriptionId to '1234-5678-9012-3456' to succeed
     *
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> postMethodGlobalValidAsync(final ServiceCallback<Void> serviceCallback);

    /**
     * POST method with subscriptionId modeled in credentials.  Set the credential subscriptionId to null, and client-side validation should prevent you from making this call
     *
     * @throws ServiceException exception thrown from REST call
     * @return the ServiceResponse object if successful.
     */
    ServiceResponse<Void> postMethodGlobalNull() throws ServiceException;

    /**
     * POST method with subscriptionId modeled in credentials.  Set the credential subscriptionId to null, and client-side validation should prevent you from making this call
     *
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> postMethodGlobalNullAsync(final ServiceCallback<Void> serviceCallback);

    /**
     * POST method with subscriptionId modeled in credentials.  Set the credential subscriptionId to '1234-5678-9012-3456' to succeed
     *
     * @throws ServiceException exception thrown from REST call
     * @return the ServiceResponse object if successful.
     */
    ServiceResponse<Void> postMethodGlobalNotProvidedValid() throws ServiceException;

    /**
     * POST method with subscriptionId modeled in credentials.  Set the credential subscriptionId to '1234-5678-9012-3456' to succeed
     *
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> postMethodGlobalNotProvidedValidAsync(final ServiceCallback<Void> serviceCallback);

    /**
     * POST method with subscriptionId modeled in credentials.  Set the credential subscriptionId to '1234-5678-9012-3456' to succeed
     *
     * @throws ServiceException exception thrown from REST call
     * @return the ServiceResponse object if successful.
     */
    ServiceResponse<Void> postPathGlobalValid() throws ServiceException;

    /**
     * POST method with subscriptionId modeled in credentials.  Set the credential subscriptionId to '1234-5678-9012-3456' to succeed
     *
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> postPathGlobalValidAsync(final ServiceCallback<Void> serviceCallback);

    /**
     * POST method with subscriptionId modeled in credentials.  Set the credential subscriptionId to '1234-5678-9012-3456' to succeed
     *
     * @throws ServiceException exception thrown from REST call
     * @return the ServiceResponse object if successful.
     */
    ServiceResponse<Void> postSwaggerGlobalValid() throws ServiceException;

    /**
     * POST method with subscriptionId modeled in credentials.  Set the credential subscriptionId to '1234-5678-9012-3456' to succeed
     *
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> postSwaggerGlobalValidAsync(final ServiceCallback<Void> serviceCallback);

}
