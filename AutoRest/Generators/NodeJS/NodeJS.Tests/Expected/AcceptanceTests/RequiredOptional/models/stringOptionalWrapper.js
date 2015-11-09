/*
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See License.txt in the project root for
 * license information.
 * 
 * Code generated by Microsoft (R) AutoRest Code Generator 0.13.0.0
 * Changes may cause incorrect behavior and will be lost if the code is
 * regenerated.
 */

'use strict';

/**
 * @class
 * Initializes a new instance of the StringOptionalWrapper class.
 * @constructor
 * @member {string} [value]
 * 
 */
function StringOptionalWrapper(parameters) {
  if (parameters !== null && parameters !== undefined) {
    if (parameters.value !== undefined) {
      this.value = parameters.value;
    }
  }    
}


/**
 * Validate the payload against the StringOptionalWrapper schema
 *
 * @param {JSON} payload
 *
 */
StringOptionalWrapper.prototype.serialize = function () {
  var payload = {};
  if (this['value'] !== null && this['value'] !== undefined) {
    if (typeof this['value'].valueOf() !== 'string') {
      throw new Error('this[\'value\'] must be of type string.');
    }
    payload['value'] = this['value'];
  }

  return payload;
};

/**
 * Deserialize the instance to StringOptionalWrapper schema
 *
 * @param {JSON} instance
 *
 */
StringOptionalWrapper.prototype.deserialize = function (instance) {
  if (instance) {
    if (instance['value'] !== undefined) {
      this['value'] = instance['value'];
    }
  }

  return this;
};

module.exports = StringOptionalWrapper;
