﻿using System.Text.Json.Serialization;

namespace RestAPIServer.Interface;

/** 
    <summary>
        
    </summary>
*/
public interface IBlacklistedDomain
{
    /** 
        <summary>
            
        </summary>
        <remarks>
            
        </remarks>
    */
    [JsonPropertyName("Id")]
    public long? id { get; }
    /** 
        <summary>
            
        </summary>
        <remarks>
            
        </remarks>
    */
    [JsonPropertyName("EmailDomain")]
    public string emaildomain { get; }
}
