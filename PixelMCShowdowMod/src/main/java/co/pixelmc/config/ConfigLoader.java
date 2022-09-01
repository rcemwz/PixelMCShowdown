package co.pixelmc.config;

import com.google.gson.Gson;

import java.io.*;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

public class ConfigLoader {
    public static class ConfigLoaderBuilder {
        private String configResource;
        private String configName;
        private String configPath;

        public ConfigLoaderBuilder usingDefaultConfig(String resource){
            this.configResource = resource;
            return this;
        }

        public ConfigLoaderBuilder ofDirectory(String directory) throws IOException {
            Files.createDirectories(Paths.get(directory));
            return this;
        }

        public ConfigLoaderBuilder usingName(String name) {
            this.configName = name;
            return this;
        }

        public ConfigLoader build() throws IOException {
            String fileContents = readResource(this.getClass(), this.configResource);

            File configFile = new File(this.configPath + "/" + this.configName);
            if (configFile.exists())
                return new ConfigLoader(configFile);

            try(FileOutputStream outputStream = new FileOutputStream(configFile)) {
                outputStream.write(fileContents.getBytes());
            }

            return new ConfigLoader(configFile);
        }
    }

    private File configFile;
    private PixelMCConfig config;

    private ConfigLoader(File configFile){
        this.configFile = configFile;
    }

    public ConfigLoader load() throws IOException{
        Gson gson = new Gson();
        config = gson.fromJson(readResource(this.getClass(), configFile.getPath()), PixelMCConfig.class);
        return this;
    }

    public PixelMCConfig get(){
        return config;
    }

    private static String readResource(Class<?> c, String path) throws IOException {
        try(InputStream inputStream = c.getResourceAsStream(path)){
            BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream));
            StringBuilder s = new StringBuilder();
            String x;
            while ((x = reader.readLine()) != null)
                s.append(x);
            return s.toString();
        }
    }
}
