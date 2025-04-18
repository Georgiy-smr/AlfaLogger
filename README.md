# AlfaLogger
Simple logging of events to the database


 ServiceCollection services = new ServiceCollection();
 services.AddAlfaLogger("appDataBase.db");
 IServiceProvider serviceProvider = services.BuildServiceProvider();
 var init = serviceProvider.GetRequiredService<LoggerInitialization>();

try
{
  testService.GoTestWork(testMessage);
}
catch (Exception e)
{
	await logger.Log(new ExceptionEvent(DateTime.Now, nameof(ItemsTests))
	{
		Exception = e,
	});
}



         
