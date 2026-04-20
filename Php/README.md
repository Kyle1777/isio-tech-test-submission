# GildedRose - PHP Version

See the [top level readme](../README.md) for general information about this exercise. This is the PHP version of the
GildedRose Test.

## Installation

The test uses:

- [8.0+](https://www.php.net/downloads.php)
- [Composer](https://getcomposer.org)

Install all the dependencies using composer

```shell script
composer install
```

## Dependencies

The project uses composer to install:

- [PHPUnit](https://phpunit.de/)
- [PHPStan](https://github.com/phpstan/phpstan)

## Folders

- `src` - contains the two classes:
    - `Item.php` - this class should not be changed
    - `GildedRose.php` - this class needs to be refactored, and the new feature added
- `tests` - contains the tests
    - `GildedRoseTest.php` - starter test.
        - Tip: ApprovalTests has been included as a dev dependency, see the PHP version of
          the [Theatrical Players Refactoring Kata](https://github.com/emilybache/Theatrical-Players-Refactoring-Kata/)
          for an example
- `Fixture`
    - `example_simulation.php` this could be used by an ApprovalTests, or run from the command line

## Fixture

To run the fixture from the php directory:

```shell
php .\fixtures\example_simulation.php 10
```

Change **10** to the desired days.

## Testing

PHPUnit is configured for testing, a composer script has been provided. To run the unit tests, from the root of the PHP
project run:

```shell script
composer tests
```
