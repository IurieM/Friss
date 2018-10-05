import { AppPage } from './app.po';
import { browser, by, element } from 'protractor';

describe('workspace-project App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('Admin User Logs in', () => {
    page.navigateTo();
    element(by.name("username")).sendKeys("Admin");
    element(by.name("password")).sendKeys("Admin");
    element(by.name('save')).click();
    browser.waitForAngular();
    let uploadButton = element(by.name('uploadButton'));
    expect(uploadButton).not.toBeNull();
  });
});
