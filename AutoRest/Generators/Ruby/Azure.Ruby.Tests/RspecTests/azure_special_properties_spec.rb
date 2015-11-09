# encoding: utf-8

$: << 'RspecTests/Generated/azure_special_properties'

require 'rspec'
require 'securerandom'

require 'azure_special_properties'

include AzureSpecialPropertiesModule

describe 'Azure Special properties behaviour' do
  before(:all) do
    @base_url = ENV['StubServerURI']
    @validSubscription = "1234-5678-9012-3456"
    @validApiVersion = "2.0"
    @unencodedPath = "path1/path2/path3"
    @unencodedQuery = "value1&q2=value2&q3=value3"

    dummyToken = 'dummy12321343423'
    @credentials = MsRest::TokenCredentials.new(dummyToken)
    @validClientId = "9C4D50EE-2D56-4CD3-8152-34347DC9F2B0"

    @client = AutoRestAzureSpecialParametersTestClient.new(@credentials, @base_url)
    @client.subscription_id = @validSubscription
  end

  # Subscription Tests
  it 'should use post parameter from credentials if ms-global not provided' do
    result = @client.subscription_in_credentials.post_method_global_not_provided_valid().value!
    expect(result.response.status).to eq(200)
  end

  it 'should use post parameter from credentials for method parameter if ms-global true' do
    result = @client.subscription_in_credentials.post_method_global_valid().value!
    expect(result.response.status).to eq(200)
  end

  it 'should use post parameter from credentials for path parameter if ms-global true' do
    result = @client.subscription_in_credentials.post_path_global_valid().value!
    expect(result.response.status).to eq(200)
  end

  it 'should use post parameter from credentials for referenced parameter if ms-global true' do
    result = @client.subscription_in_credentials.post_swagger_global_valid().value!
    expect(result.response.status).to eq(200)
  end

  it 'should use post parameter from path for method parameter if ms-global false' do
    result = @client.subscription_in_method.post_method_local_valid(@validSubscription).value!
    expect(result.response.status).to eq(200)
  end

  it 'should use post parameter from path for path parameter if ms-global false' do
    result = @client.subscription_in_method.post_path_local_valid(@validSubscription).value!
    expect(result.response.status).to eq(200)
  end

  it 'should use post parameter from path for referenced parameter if ms-global false' do
    result = @client.subscription_in_method.post_swagger_local_valid(@validSubscription).value!
    expect(result.response.status).to eq(200)
  end

  it 'should throw an exception if no post parameter if ms-global false' do
    expect { @client.subscription_in_method.post_method_local_null(nil).value! }.to raise_exception(ArgumentError)
  end

  # ApiVersion Tests
  it 'should use get parameter from credentials if ms-global not provided' do
    result = @client.api_version_default.get_method_global_not_provided_valid().value!
    expect(result.response.status).to eq(200)
  end

  it 'should use get parameter from credentials for method parameter if ms-global true' do
    result = @client.api_version_default.get_method_global_valid().value!
    expect(result.response.status).to eq(200)
  end

  it 'should use get parameter from credentials for path parameter if ms-global true' do
    result = @client.api_version_default.get_path_global_valid().value!
    expect(result.response.status).to eq(200)
  end

  it 'should use get parameter from credentials for referenced parameter if ms-global true' do
    result = @client.api_version_default.get_swagger_global_valid().value!
    expect(result.response.status).to eq(200)
  end

  it 'should use get parameter from path for method parameter if ms-global false' do
    result = @client.api_version_local.get_method_local_valid(@validApiVersion).value!
    expect(result.response.status).to eq(200)
  end

  it 'should successfully get nil value if ms-global false' do
    result = @client.api_version_local.get_method_local_null(nil).value!
    expect(result.response.status).to eq(200)
  end

  it 'should use get parameter from path for path parameter if ms-global false' do
    result = @client.api_version_local.get_path_local_valid(@validApiVersion).value!
    expect(result.response.status).to eq(200)
  end

  it 'should use get parameter from path for referenced parameter if ms-global false' do
    result = @client.api_version_local.get_swagger_local_valid(@validApiVersion).value!
    expect(result.response.status).to eq(200)
  end

  # URL Encoding Tests
  it 'should encode method parameters in path' do
    result = @client.skip_url_encoding.get_method_path_valid(@unencodedPath).value!
    expect(result.response.status).to eq(200)
  end

  it 'should encode path parameters in path' do
    result = @client.skip_url_encoding.get_path_path_valid(@unencodedPath).value!
    expect(result.response.status).to eq(200)
  end

  it 'should encode referenced parameters in path' do
    result = @client.skip_url_encoding.get_swagger_path_valid(@unencodedPath).value!
    expect(result.response.status).to eq(200)
  end

  it 'should encode method parameters in query string' do
    result = @client.skip_url_encoding.get_method_query_valid(@unencodedQuery).value!
    expect(result.response.status).to eq(200)
  end

  it 'should encode path parameters in query string' do
    result = @client.skip_url_encoding.get_path_query_valid(@unencodedQuery).value!
    expect(result.response.status).to eq(200)
  end

  it 'should encode referenced parameters in query string' do
    result = @client.skip_url_encoding.get_swagger_query_valid(@unencodedQuery).value!
    expect(result.response.status).to eq(200)
  end

  it 'should encode nil parameters in query string' do
    result = @client.skip_url_encoding.get_method_query_null(nil).value!
    expect(result.response.status).to eq(200)
  end

  it 'should allow to skip nil parameter' do
    result = @client.skip_url_encoding.get_method_query_null().value!
    expect(result.response.status).to eq(200)
  end

  # XMS Client Request Id Tests
  it 'should overwrite hard coded headers with custom headers from code' do
    headers = Hash.new
    headers['x-ms-client-request-id'] = @validClientId
    result = @client.xms_client_request_id.get(headers).value!
    expect(result.response.status).to eq(200)
    expect(result.request_id).to eq("123")
  end

  it 'should overwrite hard coded headers with custom headers from parameters' do
    result = @client.xms_client_request_id.param_get(@validClientId).value!
    expect(result.response.status).to eq(200)
    expect(result.request_id).to eq("123")
  end
  
  it 'should allow custom-named request-id headers to be used' do
    result = @client.header.custom_named_request_id(@validClientId).value!
    expect(result.response.status).to eq(200)
    expect(result.request_id).to eq("123")
  end

end