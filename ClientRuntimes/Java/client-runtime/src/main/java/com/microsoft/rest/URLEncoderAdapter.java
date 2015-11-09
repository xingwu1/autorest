/**
 *
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See License.txt in the project root for license information.
 *
 */

package com.microsoft.rest;

import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;

/**
 * An instance of this class holds a response object and a raw REST response.
 */
public class URLEncoderAdapter {
    public static String encode(String str) {
        if (str == null) {
            return null;
        }

        try {
            return URLEncoder.encode(str, "UTF-8");
        } catch (UnsupportedEncodingException ex) {
            return null;
        }
    }
}
