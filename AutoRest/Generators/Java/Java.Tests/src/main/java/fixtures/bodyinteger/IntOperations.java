/**
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See License.txt in the project root for
 * license information.
 * 
 * Code generated by Microsoft (R) AutoRest Code Generator 0.13.0.0
 * Changes may cause incorrect behavior and will be lost if the code is
 * regenerated.
 */

package fixtures.bodyinteger;

import com.microsoft.rest.ServiceCallback;
import com.microsoft.rest.ServiceException;
import com.microsoft.rest.ServiceResponse;
import com.squareup.okhttp.ResponseBody;
import retrofit.Call;
import retrofit.http.Body;
import retrofit.http.GET;
import retrofit.http.PUT;

/**
 * An instance of this class provides access to all the operations defined
 * in IntOperations.
 */
public interface IntOperations {
    /**
     * The interface defining all the services for IntOperations to be
     * used by Retrofit to perform actually REST calls.
     */
    interface IntService {
        @GET("/int/null")
        Call<ResponseBody> getNull();

        @GET("/int/invalid")
        Call<ResponseBody> getInvalid();

        @GET("/int/overflowint32")
        Call<ResponseBody> getOverflowInt32();

        @GET("/int/underflowint32")
        Call<ResponseBody> getUnderflowInt32();

        @GET("/int/overflowint64")
        Call<ResponseBody> getOverflowInt64();

        @GET("/int/underflowint64")
        Call<ResponseBody> getUnderflowInt64();

        @PUT("/int/max/32")
        Call<ResponseBody> putMax32(@Body int intBody);

        @PUT("/int/max/64")
        Call<ResponseBody> putMax64(@Body long intBody);

        @PUT("/int/min/32")
        Call<ResponseBody> putMin32(@Body int intBody);

        @PUT("/int/min/64")
        Call<ResponseBody> putMin64(@Body long intBody);

    }
    /**
     * Get null Int value
     *
     * @return the Integer object wrapped in {@link ServiceResponse} if successful.
     * @throws ServiceException the exception wrapped in ServiceException if failed.
     */
    ServiceResponse<Integer> getNull() throws ServiceException;

    /**
     * Get null Int value
     *
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> getNullAsync(final ServiceCallback<Integer> serviceCallback);

    /**
     * Get invalid Int value
     *
     * @return the Integer object wrapped in {@link ServiceResponse} if successful.
     * @throws ServiceException the exception wrapped in ServiceException if failed.
     */
    ServiceResponse<Integer> getInvalid() throws ServiceException;

    /**
     * Get invalid Int value
     *
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> getInvalidAsync(final ServiceCallback<Integer> serviceCallback);

    /**
     * Get overflow Int32 value
     *
     * @return the Integer object wrapped in {@link ServiceResponse} if successful.
     * @throws ServiceException the exception wrapped in ServiceException if failed.
     */
    ServiceResponse<Integer> getOverflowInt32() throws ServiceException;

    /**
     * Get overflow Int32 value
     *
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> getOverflowInt32Async(final ServiceCallback<Integer> serviceCallback);

    /**
     * Get underflow Int32 value
     *
     * @return the Integer object wrapped in {@link ServiceResponse} if successful.
     * @throws ServiceException the exception wrapped in ServiceException if failed.
     */
    ServiceResponse<Integer> getUnderflowInt32() throws ServiceException;

    /**
     * Get underflow Int32 value
     *
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> getUnderflowInt32Async(final ServiceCallback<Integer> serviceCallback);

    /**
     * Get overflow Int64 value
     *
     * @return the Long object wrapped in {@link ServiceResponse} if successful.
     * @throws ServiceException the exception wrapped in ServiceException if failed.
     */
    ServiceResponse<Long> getOverflowInt64() throws ServiceException;

    /**
     * Get overflow Int64 value
     *
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> getOverflowInt64Async(final ServiceCallback<Long> serviceCallback);

    /**
     * Get underflow Int64 value
     *
     * @return the Long object wrapped in {@link ServiceResponse} if successful.
     * @throws ServiceException the exception wrapped in ServiceException if failed.
     */
    ServiceResponse<Long> getUnderflowInt64() throws ServiceException;

    /**
     * Get underflow Int64 value
     *
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> getUnderflowInt64Async(final ServiceCallback<Long> serviceCallback);

    /**
     * Put max int32 value
     *
     * @param intBody the int value
     * @return the {@link ServiceResponse} object if successful.
     * @throws ServiceException the exception wrapped in ServiceException if failed.
     */
    ServiceResponse<Void> putMax32(int intBody) throws ServiceException;

    /**
     * Put max int32 value
     *
     * @param intBody the int value
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> putMax32Async(int intBody, final ServiceCallback<Void> serviceCallback);

    /**
     * Put max int64 value
     *
     * @param intBody the long value
     * @return the {@link ServiceResponse} object if successful.
     * @throws ServiceException the exception wrapped in ServiceException if failed.
     */
    ServiceResponse<Void> putMax64(long intBody) throws ServiceException;

    /**
     * Put max int64 value
     *
     * @param intBody the long value
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> putMax64Async(long intBody, final ServiceCallback<Void> serviceCallback);

    /**
     * Put min int32 value
     *
     * @param intBody the int value
     * @return the {@link ServiceResponse} object if successful.
     * @throws ServiceException the exception wrapped in ServiceException if failed.
     */
    ServiceResponse<Void> putMin32(int intBody) throws ServiceException;

    /**
     * Put min int32 value
     *
     * @param intBody the int value
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> putMin32Async(int intBody, final ServiceCallback<Void> serviceCallback);

    /**
     * Put min int64 value
     *
     * @param intBody the long value
     * @return the {@link ServiceResponse} object if successful.
     * @throws ServiceException the exception wrapped in ServiceException if failed.
     */
    ServiceResponse<Void> putMin64(long intBody) throws ServiceException;

    /**
     * Put min int64 value
     *
     * @param intBody the long value
     * @param serviceCallback the async ServiceCallback to handle successful and failed responses.
     * @return the {@link Call} object
     */
    Call<ResponseBody> putMin64Async(long intBody, final ServiceCallback<Void> serviceCallback);

}
