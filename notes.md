# MNB SOAP endpoints  #

## `GetExchangeRatesAsync` ##

It retrieves the exchange rate table corresponding to the given parameters.

### Request ###

Call `GetExchangeRatesAsync` SOAP call. 

Requires a parameter of type `GetExchangeRatesRequest`, where we passing in a `GetExchangeRatesRequestBody` object with the request parameters. This request body class has the below members in the class definition:

```csharp
    public partial class GetExchangeRatesRequestBody : object
    {
        /* ... */
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string startDate
        {
            get {return this.startDateField;}
            set {this.startDateField = value;}
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string endDate
        {
            get {return this.endDateField;}
            set {this.endDateField = value;}
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string currencyNames
        {
            get {return this.currencyNamesField;}
            set {this.currencyNamesField = value;}
        }
    }
```

Dates are entered in a `yyyy‐MM-dd` format (separated by hyphens); currency names for the `currenyNames` member are entered with their three‐letter capitalised abbreviation, separated by commas.

### Response ###



```xml
<?xml version="1.0" encoding="UTF-8" standalone="no"?>
<MNBExchangeRates>
    <Day date="2023-10-06">
        <Rate curr="CHF" unit="1">401,70</Rate>
        <Rate curr="EUR" unit="1">386,81</Rate>
        <Rate curr="GBP" unit="1">447,35</Rate>
    </Day>
    <Day date="2023-10-05">
        <Rate curr="CHF" unit="1">402,07</Rate>
        <Rate curr="EUR" unit="1">387,66</Rate>
        <Rate curr="GBP" unit="1">447,41</Rate>
    </Day>
	
    ...
	
    <Day date="2023-10-02">
        <Rate curr="CHF" unit="1">402,38</Rate>
        <Rate curr="EUR" unit="1">387,43</Rate>
        <Rate curr="GBP" unit="1">447,07</Rate>
    </Day>
</MNBExchangeRates>

```


## `GetCurrentExchangesRatesAsynnc` ##

It retrieves exchange rates for the current day of all the available currencies of the current day.

### Request ###

Call `GetCurrentExchangeRatesAsync` SOAP call. 

Requires a parameter of type `GetCurrentExchangeRatesRequest`, where we passing in a parameterless `GetCurrentExchangeRatesRequestBody` object. 



### Response ###

```xml
<?xml version="1.0" encoding="UTF-8" standalone="no"?>
<MNBCurrentExchangeRates>
    <Day date="2023-10-17">
        <Rate curr="AUD" unit="1">232,23000</Rate>
        <Rate curr="BGN" unit="1">197,19000</Rate>
        <Rate curr="BRL" unit="1">72,65000</Rate>
        <Rate curr="CAD" unit="1">268,31000</Rate>
        <Rate curr="CHF" unit="1">405,56000</Rate>
		
        ...
		
        <Rate curr="USD" unit="1">366,06000</Rate>
        <Rate curr="ZAR" unit="1">19,45000</Rate>
    </Day>
</MNBCurrentExchangeRates>
```


## `GetInfoAsync` ##

It retrieves the time interval that can be queried and lists all queriable currencies.

### Request ###

Call `GetInfoAsync` SOAP call. 

Requires a parameter of type `GetInfoRequest`, where we passing in a parameterless `GetInfoRequestBody` object. 

### Response ###

```xml
<?xml version="1.0" encoding="UTF-8" standalone="no"?>
<MNBExchangeRatesQueryValues>
    <FirstDate>1949-01-03</FirstDate>
    <LastDate>2023-10-17</LastDate>
    <Currencies>
        <Curr>HUF</Curr>
        <Curr>EUR</Curr>
        <Curr>AUD</Curr>
		
        ...
		
		<Curr>XEU</Curr>
        <Curr>XTR</Curr>
        <Curr>YUD</Curr>
    </Currencies>
</MNBExchangeRatesQueryValues>
```


