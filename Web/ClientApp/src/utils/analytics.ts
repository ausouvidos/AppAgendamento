class AnalyticsUtils {
  private trackingId = 'UA-164936219-1';

  public gtagCommand(command: string, ...args: any[]) {
    try {
      gtag(command, ...args);
    } catch (error) {
      console.error(error);
    }
  }

  public pageView(path: string) {
    this.gtagCommand('config', this.trackingId, { page_path: path });
  }

  public sendEvent(category: string, action: string, label?: string, value?: string) {
    this.gtagCommand('event', action, {
      event_category: category,
      event_label: label,
      value,
    });
  }
}

export default new AnalyticsUtils();
