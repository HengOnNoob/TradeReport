README

# Trade Report Generator

This project is a command-line application that generates reports from a CSV file containing trade data. The application takes in arguments for filtering the data and generates reports based on the specified criteria.

## Getting Started

### Prerequisites
- A CSV is stored at the same directory of the program (It's a dummy records to pretend retrieving from database)
- A CSV file containing trade data in the following format:

TradeReference,ProductId,ProductName,TradeDate,Quantity,BuySellIndicator,Price,UnderlyingAsset,ExpiryDate,OptionType,StrikePrice,FixedRate,Notional,EffectiveDate,EndDate,Broker
T-FWD-1,1,AUDNZD FRD Exp14Jul2021,20200408,1000000,B,1.067591,,,14-Jul-2021,,,,,,Broker A
...

### Installation

1. Clone the repository to your local machine.
2. Open a terminal and navigate to the project directory.
3. Build the project.

### Usage

To run the application, navigate to the project directory and run program. The application takes in arguments for filtering the data and generating reports based on the specified criteria.

```console
- Reach to the program directory
TradeReport.exe <productType> <brokerName> <tradeDate>
```

- `productType` (optional): The type of product to filter by. Valid options are `FWD`, `BF`, `IRS`, `CDS`, and `OPT`.
- `brokerName` (optional): The name of the broker to filter by. Valid brokers are `Broker A`, `Broker B` and `Broker C`
- `tradeDate` (optional): The trade date to filter by. Must be in the format `yyyyMMdd` or `yyyy-MM-dd`.

With the arguement "TEST", the application will generate varies report in the CSV file.
If the type of product or brokerName is invalid, it will return the header with empty records

## Features

The application supports the following features:

- Filtering by product type, broker name, and trade date.
- Generating a report of all trades.
- Generating a report of trades by product type.
- Generating a report of trades by broker.
- Generating a report of trades by trade date.