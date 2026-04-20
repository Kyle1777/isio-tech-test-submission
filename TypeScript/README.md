# Gilded Rose

This is the Gilded Rose in TypeScript.

## Getting started

Ensure you have NodeJS Installed

Install dependencies

```sh
npm install
```

## Run the unit tests from the Command-Line

Unit tests are developed using the Jest framework:

```sh
npm run test:jest
```

To run all tests in watch mode

```sh
npm run test:jest:watch
```


## Run the example simulation from the Command-Line

_You may need to install `ts-node` globally_ 

```sh
npx ts-node test/example_simulation.ts
```

Or with number of days as args:
```sh
npx ts-node test/example_simulation.ts 10
```
