# SpyMaster/SpyLens Pact Example

This is an example of using pact.net for consumer driven contracts

This is meant to serve as a basic example, and is part of a presentation about PACT.

## Prerequisites

- dotnet core sdk
- locally running pact broker (see README in broker directory for instructions on how to set this up, doesn't take long)


## Overview 

- MI6 uses an internal system called SpyStore that allows them to manage the personal data for their agents
Agent information is used by “handlers” in the field

- In order to make it easier to get this information to handlers, MI6 wants to build a new application called SpyLens 
SpyLen will allow handlers to look up agent details by their “agent number”

- An agent only trusts a handler if they knows the spy's real name, date of birth and age

## Repo Contents 

### Consumer/

This is the consumer side of the pact, and consists of the PACT tests, and the ApiClient that will make the requests to the provider

### Provider/

The provider side of the solution, which consists of a web api and

### Broker/

Set of instructions for setting up a local PACT broker for demo purposes. This is needed as the current tests push and pull from the broker, rather than sharing the pacts via disk


## Running pacts

1. First run the tests on the consumer to produce the pact file and have it pushed to the broker

- you can use a test runner like resharper
- you can use dotnet test

These should pass - you can see output in the pacts directory, and logs for troubleshooting in the logs directory

2. Check http://localhost - you should see a broker and a published pact in the list

3. Run the provider tests
