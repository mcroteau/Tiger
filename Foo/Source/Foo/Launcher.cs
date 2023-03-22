using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Collections;
using System.Threading;
using Skyline;
using Skyline.Model;
using Skyline.Schemes;
using Skyline.Security;

using Foo.Model;

using LiteDB;

namespace Foo{
    class Launcher{

        public static int Main(String[] args){
            
            DatabaseSetup databaseSetup = new DatabaseSetup();
            databaseSetup.clean();
            databaseSetup.setup();

            ApplicationAttributes applicationAttributes = new ApplicationAttributes();
            applicationAttributes.getAttributes().Add("abc", "123");
            applicationAttributes.getAttributes().Add("db", "Ocean.db");

            SkylineRunnable skyline = new SkylineRunnable(4000);
            skyline.setNumberOfPartitions(30);
            skyline.setNumberOfRequestExecutors(70);

            PersistenceConfig persistenceConfig = new PersistenceConfig();

            ViewConfig viewConfig = new ViewConfig();
            viewConfig.setResourcesPath("Assets");
            viewConfig.setRenderingScheme(RenderingScheme.RELOAD_EACH_REQUEST);

            skyline.setSecurityAccessType(new AuthAccess().GetType());

            SecurityManager manager = new SecurityManager(new AuthAccess(new DataTransferObject(persistenceConfig)));
            manager.signin("abc@plsar.net", "effort.", new NetworkRequest(), new NetworkResponse());

            skyline.setApplicationAttributes(applicationAttributes);
            skyline.setPersistenceConfig(persistenceConfig);
            skyline.setViewConfig(viewConfig);

            skyline.start();
            return 0;
        }
    }
}

