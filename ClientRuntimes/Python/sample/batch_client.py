﻿

from clientruntime.msrest import ServiceClient, Configuration
from pool_manager import PoolManager

class BatchClient(ServiceClient):

    def __init__(self, credentials, config=Configuration()):

        self._client = ServiceClient(credentials, config)
        self.pools = PoolManager(self._client)