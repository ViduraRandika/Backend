﻿using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace CovidSafe.DAL.Repositories.Cosmos.Client
{
    /// <summary>
    /// Cosmos database context
    /// </summary>
    public class CosmosContext
    {
        /// <summary>
        /// <see cref="IOptionsMonitor{T}"/> reference for <see cref="CosmosSchemaConfigurationSection"/>
        /// </summary>
        /// <remarks>
        /// Enables configuration change detection without restarting service
        /// </remarks>
        private IOptionsMonitor<CosmosSchemaConfigurationSection> _schemaConfig { get; set; }
        /// <summary>
        /// <see cref="CosmosClient"/> instance
        /// </summary>
        public CosmosClient Client { get; private set; }
        /// <summary>
        /// <see cref="Database"/> reference
        /// </summary>
        public Database Database { get; private set; }
        /// <summary>
        /// <see cref="CosmosSchemaConfigurationSection"/> instance
        /// </summary>
        public CosmosSchemaConfigurationSection SchemaOptions
        {
            get
            {
                return this._schemaConfig.CurrentValue;
            }
        }

        /// <summary>
        /// Creates a new <see cref="CosmosContext"/> instance
        /// </summary>
        /// <param name="connectionFactory">Database connection factory instance</param>
        /// <param name="schemaConfig">Schema configuration provider</param>
        public CosmosContext(CosmosConnectionFactory connectionFactory, IOptionsMonitor<CosmosSchemaConfigurationSection> schemaConfig)
        {
            // Set local variables
            this._schemaConfig = schemaConfig;
            this.Client = connectionFactory.GetClient();

            // Create Database object reference
            this.Database = this.Client.GetDatabase(this.SchemaOptions.DatabaseName);
        }

        /// <summary>
        /// Gets a reference to the specified <see cref="Container"/> from the 
        /// local <see cref="CosmosSchemaConfigurationSection"/> instance
        /// </summary>
        /// <param name="containerName">Target <see cref="Container"/> name</param>
        /// <returns><see cref="Container"/> reference</returns>
        public Container GetContainer(string containerName)
        {
            return this.Database.GetContainer(containerName);
        }
    }
}
