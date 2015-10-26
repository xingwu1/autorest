﻿
from runtime.msrest.exceptions import InvalidOperationError
from runtime.msrest.utils import *
from ..batch_constants import *
from .shared import *



class DetailLevel(object):
    
    def __init__(self):
        self.filter_clause = None
        self.select_clause = None
        self.expand_clause = None

    def get_parameters(self):
        params = {}

        if self.select_clause:
            params['$select'] = self.select_clause

        if self.expand_clause:
            params['$expand'] = self.expand_clause

        if self.filter_clause:
            params['$filter'] = self.filter_clause

        return params


class PoolParameters(object):

    _required = ['name']

    _attribute_map = {
            'id':{'key':'id', 'type':'str'},
            'certificate_references': {'key':'certificateReferences', 'type':'[Certificate]'},
            'metadata': {'key':'metadata', 'type':'{str}'},
            'name': {'key':'displayName', 'type':'str'},
            'vm_size': {'key':'vmSize', 'type':'str'},
            'resize_timeout': {'key':'resizeTimeout', 'type':'time'},
            'target_dedicated': {'key':'targetDedicated', 'type':'int'},
            'enable_auto_scale': {'key':'enableAutoScale', 'type':'bool'},
            'auto_scale_formula': {'key':'autoScaleFormula', 'type':'str'},
            'communication': {'key':'enableInterNodeCommunication', 'type':'str'},
            'start_task': {'key':'startTask', 'type':'StartTask'},
            'max_tasks_per_node': {'key':'maxTasksPerNode', 'type':'int'},
            'scheduling_policy': {'key':'taskSchedulingPolicy', 'type':'TaskSchedulePolicy'},
            'os_family': {'key':'osFamily', 'type':'str'},
            'target_os_version': {'key':'targetOSVersion', 'type':'str'},
        }
    
    def __init__(self, *args, **kwargs):

        self._manager = kwargs.get('manager')
        
        self.id = None
        self.certificate_references = []
        self.metadata = {}
        self.name = None
        self.vm_size = None
        self.resize_timeout = None
        self.target_dedicated = None
        self.enable_auto_scale = None
        self.auto_scale_formula = None
        self.communication = None
        self.start_task = None
        self.max_tasks_per_node = None
        self.scheduling_policy = None
        self.os_family = None
        self.target_os_version = None

        for k in kwargs:
            if hasattr(self, k):
                setattr(self, k, kwargs[k])


    def add(self):
        response = self._manager.add(self)
        return None


class Page(object):

    _required = []

    _attribute_map = {
        'next_link': {'key':None, 'type':'str'}
        }

    def __init__(self, **kwargs):

        self.next_link = None

        for k in kwargs:
            if hasattr(self, k):
                setattr(self, k, kwargs[k])

class Pool(object):

    _attribute_map = {
            'id':{'key':'id', 'type':'str'},
            'certificate_references': {'key':'certificateReferences', 'type':'[Certificate]'},
            'metadata': {'key':'metadata', 'type':'{str}'},
            'name': {'key':'displayName', 'type':'str'},
            'vm_size': {'key':'vmSize', 'type':'str'},
            'resize_timeout': {'key':'resizeTimeout', 'type':'time'},
            'target_dedicated': {'key':'targetDedicated', 'type':'int'},
            'enable_auto_scale': {'key':'enableAutoScale', 'type':'bool'},
            'auto_scale_formula': {'key':'autoScaleFormula', 'type':'str'},
            'communication': {'key':'enableInterNodeCommunication', 'type':'str'},
            'start_task': {'key':'startTask', 'type':'StartTask'},
            'max_tasks_per_node': {'key':'maxTasksPerNode', 'type':'int'},
            'scheduling_policy': {'key':'taskSchedulingPolicy', 'type':'TaskSchedulePolicy'},
            'os_family': {'key':'osFamily', 'type':'str'},
            'target_os_version': {'key':'targetOSVersion', 'type':'str'},
            'url': {'key':'url', 'type':'str'},
            'e_tag': {'key':'eTag', 'type':'str'},
            'last_modified': {'key':'lastModifed', 'type':'iso-date'},
            'creation_time': {'key':'creationTime', 'type':'iso-date'},
            'state': {'key':'state', 'type':'str'},
            'state_transition_time': {'key':'stateTransitionTime', 'type':'iso-date'},
            'allocation_state': {'key':'allocationState', 'type':'str'},
            'allocation_state_transition_time': {'key':'allocationStateTransitionTime', 'type':'iso-date'},
            'resize_error': {'key':'resizeError', 'type':'ResizeError'},
            'current_dedicated': {'key':'currentDedicated', 'type':'int'},
            'auto_scale_run': {'key':'autoScaleRun', 'type':'AutoScaleRun'},
            'stats': {'key':'stats', 'type':'ResourceStats'},
            'current_os_version': {'key':'currentOSVersion', 'type':'str'},
        }

    def __init__(self, **kwargs):
        self._manager = kwargs.get('manager')

    def _update(self, new_pool):
        for attr in self.attribute_map:
            setattr(self, attr, getattr(new_pool, attr))

    def update(self):
        response = self._manager.get(self.id)
        self._update(response.pool)

    def delete(self):
        response = self._manager.delete(self.id)

    def disable_auto_scale(self):
        response = self._manager.disable_auto_scale(self.id)

    def enable_auto_scale(self, auto_scale_formula):
        parameters = PoolAutoScale()
        parameters.auto_scale_formula = auto_scale_formula
        response = self._manager.enable_auto_scale(parameters, self.id)

    def evaluate_auto_scale(self, auto_scale_formula):
        parameters = PoolAutoScale()
        parameters.auto_scale_formula = auto_scale_formula
        response = self._manager.evaluate_auto_scale(parameters, self.id)

    def patch(self, certificate_references=[], metadata={}, start_task=None):
        parameters = PoolProperties()
        parameters.certificate_references = certificate_references
        parameters.metadata = metadata
        parameters.start_task = start_task
        response = self._manager.patch(parameters, self.id)

    def resize(self, resize_timeout=None, target_dedicated=None, tvm_deallocation=None):
        parameters = PoolResize()
        paramters.resize_timeout = resize_timeout
        parameters.target_dedicated = target_dedicated
        parameters.tvm_deallocation_option = tvm_deallocation
        response = self._manager.resize(parameters, self.id)

    def stop_resize(self):
        response = self._manager.stop_resize(self.id)

    def update_properties(self, certificate_references=[], metadata={}, start_task=None):
        parameters = PoolProperties()
        parameters.certificate_references = certificate_references
        parameters.metadata = metadata
        parameters.start_task = start_task
        response = self._manager.update_properties(parameters, self.id)

    def upgrade_os(self, target_os_version):
        parameters = PoolOS()
        parameters.target_os_version = target_os_version
        response = self._manager.upgrade_os(parameters, self.id)


class PoolAutoScale(object):

    _required = ['auto_scale_formula']

    _attribute_map = {
                'auto_scale_formula': {'key':'autoScaleFormula', 'type':'str'}
                }

    def __init__(self, **kwargs):

        self.auto_scale_formula = None
        for k in kwargs:
            if hasattr(self, k):
                setattr(self, k, kwargs[k])


class PoolProperties(object):

    _required = []

    _attribute_map = {
            'certificate_references': {'key':'certificate_references', 'type':'[Certificate]'},
            'metadata': {'key':'metadata', 'type':'{str}'},
            'start_task': {'key':'StartTask', 'type':'StartTask'}
        }
    
    def __init__(self, **kwargs):

        self.certificate_references = []
        self.metadata = []
        self.start_task = None

        for k in kwargs:
            if hasattr(self, k):
                setattr(self, k, kwargs[k])


class PoolResize(object):

    _required = []

    _attribute_map = {
            'resize_timeout': {'key':'resizeTimeout', 'type':'time'},
            'target_dedicated': {'key':'targetDedicated', 'type':'int'},
            'tvm_deallocation_option': {'key':'tvmDeallocationOption', 'type':'str'}
        }
    
    def __init__(self, **kwargs):

        self.target_dedicated = None
        self.resize_timeout = None
        self.tvm_deallocation_option = None

        for k in kwargs:
            if hasattr(self, k):
                setattr(self, k, kwargs[k])


class PoolOS(object):

    _required = ['target_os_version']

    _attribute_map = {
            'target_os_version': {'key':'targetOSVersion', 'type':'str'}
        }
    
    def __init__(self, **kwargs):

        self.target_os_version = None

        for k in kwargs:
            if hasattr(self, k):
                setattr(self, k, kwargs[k])

class TaskSchedulePolicy(object):

    _required = ['node_fill_type']

    _attribute_map = {
        'node_fill_type': {'key':'nodeFillType', 'type':'str'}
        }

    def __init__(self, **kwargs):

        self.node_fill_type = None

        for k in kwargs:
            if hasattr(self, k):
                setattr(self, k, kwargs[k])
 