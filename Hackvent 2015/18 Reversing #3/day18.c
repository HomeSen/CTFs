function _main {
    var_18 = [[NSAutoreleasePool alloc] init];
    [NSApplication sharedApplication];
    var_28 = [[NSUserDefaults standardUserDefaults] stringForKey:@"key"];
    if ([var_28 length] == 0x1d) {
            var_30 = [var_28 componentsSeparatedByString:@"-"];
            var_50 = [[[[var_30 objectAtIndex:0x0] dataUsingEncoding:0x4] base64EncodedStringWithOptions:0x0] MD5String];
            var_70 = [[[[var_30 objectAtIndex:0x1] dataUsingEncoding:0x4] base64EncodedStringWithOptions:0x0] MD5String];
            var_90 = [[[[var_30 objectAtIndex:0x2] dataUsingEncoding:0x4] base64EncodedStringWithOptions:0x0] MD5String];
            var_B0 = [[[[var_30 objectAtIndex:0x3] dataUsingEncoding:0x4] base64EncodedStringWithOptions:0x0] MD5String];
            var_D0 = [[[[var_30 objectAtIndex:0x4] dataUsingEncoding:0x4] base64EncodedStringWithOptions:0x0] MD5String];
            var_F0 = [[[[var_30 objectAtIndex:0x5] dataUsingEncoding:0x4] base64EncodedStringWithOptions:0x0] MD5String];
            if ((((sign_extend_64([var_50 isEqualToString:@"fab3e420d6d8a17b53b23ca4bb01866b"]) != 0x0) && (sign_extend_64([var_70 isEqualToString:@"189f56eea9a9ba305dffa8425ba20048"]) != 0x0)) 
				&& (sign_extend_64([var_90 isEqualToString:@"2335667c646346b38c8f0f47b13fab13"]) != 0x0)) && (sign_extend_64([var_B0 isEqualToString:@"f4709a7eef9d703920b910fc734b151c"]) != 0x0)) {
                    if (sign_extend_64([var_D0 isEqualToString:@"b74e57f21f5a315550a9e2f6869d4e44"]) != 0x0) {
                            if (sign_extend_64([var_F0 isEqualToString:@"40abc257b6f0e0420dc9ae9ba19c8c8c"]) != 0x0) {
                                    var_F8 = [[NSAlert alloc] init];
                                    [var_F8 setMessageText:@"HACKVent 2015"];
                                    [var_F8 setInformativeText:@"Yay, this is the correct key!"];
                                    [var_F8 addButtonWithTitle:@"Ok"];
                                    [var_F8 runModal];
                            }
                    }
            }
    }
    [var_18 release];
    return 0x0;
}
