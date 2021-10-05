# TaxAdvocate
## Generates tax advices based on financial data sets

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

## Features

- Imports financial JSON payloads into Entity Framework InMemory database
- Exposes a RESTful WebApi for client applications to consume
- Includes MVC web app to display financial overview
- Financial advice indicators can be selected and a list of matching clients returned, both from webapi and mvc

## Solution Projects

The following projects are included in the solution:

| Project | Description |
| ------ | ------ |
| TaxAdvocate.Business | Contains all business logic |
| TaxAdvocate.Data | Contains InMemory database context and json payloads |
| TaxAdvocate.WebApi | RESTful web api exposing data for client applications |
| TaxAdvocate.Web | MVC web application for display purposes |
| TaxAdvocate.Business.Tests | Unit test project for testing the business layer |
| TaxAdvocate.WebApi.Tests | Unit test project for testing the web api layer |

## Evaluators

Adding a new type of advice indicator is as simple as creating a new class implementing IEvaluator.
Both Web api and the MVC web app will find the new evaluator and add it to the results.
