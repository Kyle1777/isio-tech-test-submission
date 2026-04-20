import { Item, GildedRose } from '../app/gilded-rose';

describe('Gilded Rose', () => {
  it('example test', () => {
    const gildedRose = new GildedRose([new Item('foo', 0, 0)]);
    const items = gildedRose.updateQuality();
    expect(items[0].name).toBe('fixme');
  });
});
